using MovieManager.Data.DBConfig;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;

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
        public async Task<List<Data.DataModels.Movie>> SearchMovieTitleToList(string SEARCH_NAME)
        {
            var results = await tmdbClient.SearchMovieAsync(SEARCH_NAME);

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
        public async Task<List<Data.DataModels.Movie>> SearchShowTitleToList(string SEARCH_NAME)
        {
            var results = await tmdbClient.SearchTvShowAsync(SEARCH_NAME);

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
        public async Task<MovieCardViewModel> SearchApiWithMovieID(int id)
        {
            var movie = await tmdbClient.GetMovieAsync(id, MovieMethods.Videos);

            if (movie == null)
            {
                return null;
            }
            if (movie.Title == null || movie.PosterPath == null || movie.Overview == null) { return null; }
            Console.WriteLine($"Found: {movie.Title}");

            //Get Credits
            var credits = await tmdbClient.GetMovieCreditsAsync(id);
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
        public async Task<ShowCardViewModel> SearchApiWithShowID(int id)
        {
            var show = await tmdbClient.GetTvShowAsync(id, TMDbLib.Objects.TvShows.TvShowMethods.Videos);

            if (show == null) 
            {
                return null;
            }
            //Get Credits
            var credits = await tmdbClient.GetTvShowCreditsAsync(id);
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
        public async Task<ActorViewModel> GetActorWithID(int id)
        {
            Person actor = new Person();
            MovieCredits credits = new MovieCredits();
            try
            {
                actor = await tmdbClient.GetPersonAsync(id);
                credits = await tmdbClient.GetPersonMovieCreditsAsync(id);
            }
            catch (Exception e) { e.ToString(); };

            if (credits == null) //has no movie credits -> get and display show credits
            {
                var creditsShow = new TvCredits();
                var model = new ActorViewModel();
                try
                {
                    creditsShow = await tmdbClient.GetPersonTvCreditsAsync(id);
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
        public async Task<List<ReviewBase>> GetReviewWithMovieID(int id)
        {
             var reviews = await tmdbClient.GetMovieReviewsAsync(id);

             return reviews.Results;
        }
        public async Task<List<ReviewBase>> GetReviewWithShowID(int id)
        {
            var reviews = await tmdbClient.GetTvShowReviewsAsync(id);

            return reviews.Results;
        }

        //Get Trailers
        public async Task<string> GetMovieTrailer(int id)
        {
            var trailer = await tmdbClient.GetMovieVideosAsync(id);
            var res = trailer.Results.FirstOrDefault();
            string ytLink = String.Empty;

            if(res.Site == "YouTube")
            {
                var ytKey = res.Key;
                var partial = "https://www.youtube.com/watch?v=";
                ytLink = partial + ytKey;
            }
            return ytLink;
        }
        public async Task<string> GetShowTrailer(int id)
        {
            var trailer = await tmdbClient.GetTvShowVideosAsync(id);
            var res = trailer.Results.FirstOrDefault();
            string ytLink = String.Empty;

            if (res.Site == "YouTube")
            {
                var ytKey = res.Key;
                var partial = "https://www.youtube.com/watch?v=";
                ytLink = partial + ytKey;
            }
            return ytLink;
        }


        //these 2 are also static to avoid circular reference in SaveMovieToDbObjectService service
        //this can be optimized
        public static async Task<string> GetMovieTrailerStatic(int id)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            var trailer = await client.GetMovieVideosAsync(id);
            var res = trailer.Results.FirstOrDefault();
            var ytKey = res.Key;
            if (res.Site == "YouTube")
            {
                return ytKey;                
            }
            return "notrailer";
        }
        public static async Task<string> GetShowTrailerStatic(int id)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            var trailer = await client.GetTvShowVideosAsync(id);
            var res = trailer.Results.FirstOrDefault();
            if (trailer != null && res.Key != null)
            {
                var ytKey = res.Key;

                if (res.Site == "YouTube")
                {
                    return ytKey;
                }
            }
            return "notrailer";
        }
    }
}
