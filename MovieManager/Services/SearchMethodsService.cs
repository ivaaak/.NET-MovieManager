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

        //SearchResult
        public List<Data.DataModels.Movie> SearchMovieTitleToList(string SEARCH_NAME)
        {
            SearchContainer<SearchMovie> results = tmdbClient.SearchMovieAsync(SEARCH_NAME).Result;

            List<Data.DataModels.Movie> dbMovies = new List<Data.DataModels.Movie>();

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
            
            if(results.Results.Count() == 0)
            {
                return null;
            }
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



        //MovieCard in MovieController 
        public MovieCardViewModel SearchApiWithMovieID(int id)
        {
            Movie movie = tmdbClient.GetMovieAsync(id, MovieMethods.Videos).Result;

            if (movie == null)
            {
                return null;
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

        //ShowCard
        public ShowCardViewModel SearchApiWithShowID(int id)
        {
            var show = tmdbClient.GetTvShowAsync(id, TMDbLib.Objects.TvShows.TvShowMethods.Videos).Result;

            if (show == null) 
            {
                return null;
            }
            //Get Credits
            var credits = tmdbClient.GetTvShowCreditsAsync(id).Result;
            var people = new List<TMDbLib.Objects.TvShows.Cast>();

            foreach (var person in credits.Cast)
            {
                if (person != null && person.ProfilePath != null) //dont save people with no images
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



        //Used for ActorCard  (MovieController)
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

            if (credits == null) //has no movie credits -> get and display show credits
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
            else //display movie credits (roles)
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

        //Get Trailers
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


        //these 2 are also static to avoid circular reference in SaveMovieToDbObjectService service
        //this can be optimized
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
            if(trailer != null && trailer.Key != null)
            {
                var ytKey = trailer.Key;

                if (trailer.Site == "YouTube")
                {
                    return ytKey;
                }
            }
           
            return "notrailer";
        }
    }
}
