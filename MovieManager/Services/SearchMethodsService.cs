using MovieManager.Data.DBConfig;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class SearchMethodsService : ISearchMethodsService
    {
        private ISaveMovieToDbObjectService saveMovieToDbObjectService;
        private TMDbClient tmdbClient;

        public SearchMethodsService(ISaveMovieToDbObjectService service) 
        {
            saveMovieToDbObjectService = service;
            tmdbClient = new TMDbClient(Configuration.APIKey);
        }


        public List<Data.DataModels.Movie> SearchMovieTitleToList(string SEARCH_NAME)
        {
            SearchContainer<SearchMovie> results = tmdbClient.SearchMovieAsync(SEARCH_NAME).Result;

            List<Data.DataModels.Movie> dbMovies = new List<Data.DataModels.Movie>();

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
            
            foreach (var movie in results.Results)
            {
                if(movie != null) 
                {
                    dbMovies.Add(saveMovieToDbObjectService.SearchMovieApiToObject(movie));
                }
            }
            return dbMovies;
        }

        public List<Data.DataModels.Movie> SearchShowTitleToList(string SEARCH_NAME)
        {
            SearchContainer<SearchTv> results = tmdbClient.SearchTvShowAsync(SEARCH_NAME).Result;

            List<Data.DataModels.Movie> dbShows = new List<Data.DataModels.Movie>();

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            foreach (var movie in results.Results)
            {
                if (movie != null)
                {
                    dbShows.Add(saveMovieToDbObjectService.SearchShowApiToObject(movie));
                }
            }
            return dbShows;
        }



        //CALLED IN MovieCard(int id) in MovieController -> 
        public MovieCardViewModel SearchApiWithMovieID(int id)
        {
            Movie movie = tmdbClient.GetMovieAsync(id, MovieMethods.Videos).Result;

            if (movie == null)
            {
                throw new InvalidOperationException($"Cant find a movie with ID = {id}");
            }
            if (movie.Title == null || movie.PosterPath == null || movie.Overview == null) { return null; }
            Console.WriteLine($"Found: {movie.Title}");

            //Get Credits
            var credits = tmdbClient.GetMovieCreditsAsync(id).Result;
            var people = new List<Cast>();
            foreach (var person in credits.Cast)
            {
                if (person != null && person.ProfilePath != null)
                {
                    people.Add(person);
                }
            }

            var movieModel = new MovieCardViewModel()
            {
                Movie = movie,
                MovieActorsList = people
            };
            return movieModel;
        }


        public ShowCardViewModel SearchApiWithShowID(int id)
        {
            var show = tmdbClient.GetTvShowAsync(id, TMDbLib.Objects.TvShows.TvShowMethods.Videos).Result;

            if (show == null) 
            {
                throw new InvalidOperationException($"Cant find a show with ID = {id}");
            }
            //Get Credits
            var credits = tmdbClient.GetTvShowCreditsAsync(id).Result;
            var people = new List<TMDbLib.Objects.TvShows.Cast>();

            foreach (var person in credits.Cast)
            {
                if (person != null && person.ProfilePath != null) //dont save null images
                {
                    TMDbLib.Objects.TvShows.Cast personShowCast = new TMDbLib.Objects.TvShows.Cast()
                    {
                        Id = person.Id,
                        Name = person.Name,
                        ProfilePath = person.ProfilePath,
                    };

                    people.Add(personShowCast);
                }
            }

            var model = new ShowCardViewModel()
            {
                Show = show,
                ShowActorsList = people,
            };
            return model;
        }



        //Used in MovieController for ActorCard
        public ActorViewModel GetActorWithID(int id)
        {
            Person actor = new Person();
            MovieCredits credits = new MovieCredits();
            try
            {
                actor = tmdbClient.GetPersonAsync(id).Result;
                credits = tmdbClient.GetPersonMovieCreditsAsync(id).Result;
            }
            catch (Exception e) { e.ToString(); };

            if (credits == null) //show
            {
                var creditsShow = new TvCredits();
                var model = new ActorViewModel();
                try
                {
                    creditsShow = tmdbClient.GetPersonTvCreditsAsync(id).Result;
                    model = new ActorViewModel()
                    {
                        Person = actor,
                        TvCredits = creditsShow,
                        PhotoUrl = SaveMovieToDbObjectService.BuildImageURL(actor.ProfilePath),
                    };
                }
                catch (Exception e) { e.ToString(); };

                return model;
            }
            else //movie
            {
                var model = new ActorViewModel();
                try
                {
                    model = new ActorViewModel()
                    {
                        Person = actor,
                        MovieCredits = credits,
                        PhotoUrl = SaveMovieToDbObjectService.BuildImageURL(actor.ProfilePath),
                    };
                }
                catch (Exception e) { e.ToString(); };

                return model;
            }
        }


        //Get Reviews
        public List<ReviewBase> GetReviewWithMovieID(int id)
        {
             var reviews = tmdbClient.GetMovieReviewsAsync(id).Result.Results;

             return reviews;
        }
        public List<ReviewBase> GetReviewWithShowID(int id)
        {
            var reviews = tmdbClient.GetTvShowReviewsAsync(id).Result.Results;

            return reviews;
        }

        public string GetMovieTrailer(int id)
        {
            var trailer = tmdbClient.GetMovieVideosAsync(id).Result.Results.FirstOrDefault();
            string ytLink = String.Empty;

            if(trailer.Site == "YouTube")
            {
                var ytKey = trailer.Key;
                var partial = "https://www.youtube.com/watch?v=";
                ytLink = partial + ytKey;
            }
            return ytLink;
        }

        public string GetShowTrailer(int id)
        {
            var trailer = tmdbClient.GetTvShowVideosAsync(id).Result.Results.FirstOrDefault();
            string ytLink = String.Empty;

            if (trailer.Site == "YouTube")
            {
                var ytKey = trailer.Key;
                var partial = "https://www.youtube.com/watch?v=";
                ytLink = partial + ytKey;
            }
            return ytLink;
        }


        //these 2 are static to avoid circular reference in SaveMovieToDbObjectService service
        public static string GetMovieTrailerStatic(int id)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            var trailer = client.GetMovieVideosAsync(id).Result.Results.FirstOrDefault();
            var ytKey = trailer.Key;
            if (trailer.Site == "YouTube")
            {
                return ytKey;                
            }
            return "notrailer";
        }

        public static string GetShowTrailerStatic(int id)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            var trailer = client.GetTvShowVideosAsync(id).Result.Results.FirstOrDefault();
            var ytKey = trailer.Key;

            if (trailer.Site == "YouTube")
            {
                return ytKey;
            }
            return "notrailer";
        }



        public List<Data.DataModels.Movie> SearchResultCombined(string SEARCH_NAME)
        {
            var CombinedResultsList = new List<Data.DataModels.Movie>();
            var MovieResultsList = SearchMovieTitleToList(SEARCH_NAME);
            var ShowResultsList = SearchShowTitleToList(SEARCH_NAME);

            CombinedResultsList.AddRange(MovieResultsList);
            CombinedResultsList.AddRange(ShowResultsList);

            return CombinedResultsList;
        }

        //Used in Debug - FillDatabase
        public void SearchMovieTitleAndSaveToDb(string SEARCH_NAME, AddToDbService addToDb)
        {
            SearchContainer<SearchMovie> results = tmdbClient.SearchMovieAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            addToDb.AddMovies(results);
        }
        public void SearchShowTitleAndSaveToDb(string SEARCH_NAME, AddToDbService addToDb)
        {
            SearchContainer<SearchTv> results = tmdbClient.SearchTvShowAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            addToDb.AddShows(results);
        }
    }
}
