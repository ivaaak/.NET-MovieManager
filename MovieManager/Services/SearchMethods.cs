using MovieManager.Data.DBConfig;
using MovieManager.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class SearchMethods
    {
        
        public static List<Data.DataModels.Movie> SearchResultCombined(string SEARCH_NAME)
        {
            var CombinedResultsList = new List<Data.DataModels.Movie>();
            var MovieResultsList = SearchMovieTitleToList(SEARCH_NAME);
            var ShowResultsList = SearchShowTitleToList(SEARCH_NAME);

            CombinedResultsList.AddRange(MovieResultsList);
            CombinedResultsList.AddRange(ShowResultsList);

            return CombinedResultsList;
            //return show and movie results in a single list - not used atm
        }


        //User in MovieController for SearchResults
        public static List<Data.DataModels.Movie> SearchMovieTitleToList(string SEARCH_NAME)
            //search and return a Movie list
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchMovie> results = client.SearchMovieAsync(SEARCH_NAME).Result;

            List<Data.DataModels.Movie> dbMovies = new List<Data.DataModels.Movie>();

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");
            
            foreach (var movie in results.Results)
            {
                if(movie != null) 
                {
                    dbMovies.Add(SaveMovieToDbObject.MovieApiToObject(movie));
                }
            }

            client.Dispose();

            return dbMovies;
        }

        public static List<Data.DataModels.Movie> SearchShowTitleToList(string SEARCH_NAME)
            //search and return a Show list
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchTv> results = client.SearchTvShowAsync(SEARCH_NAME).Result;

            List<Data.DataModels.Movie> dbShows = new List<Data.DataModels.Movie>();

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            foreach (var movie in results.Results)
            {
                if (movie != null)
                {
                    dbShows.Add(SaveMovieToDbObject.ShowApiToObject(movie));
                }
            }

            client.Dispose();

            return dbShows;
        }


        public static void SearchMovieTitleAndSaveToDb(string SEARCH_NAME)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchMovie> results = client.SearchMovieAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            AddToDb.AddMovies(results);

            client.Dispose();
        }

        public static void SearchShowTitleAndSaveToDb(string SEARCH_NAME)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchTv> results = client.SearchTvShowAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            AddToDb.AddShows(results);

            client.Dispose();
        }



        //CALLED IN MovieCard(int id) in MovieController
        public static MovieCardViewModel SearchApiWithMovieID(int id)
            //should also take a string MediaType to differentiate between show and movie
            //search based on Id and Media type - (id 500 can be a show or movie)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);
            TMDbLib.Objects.TvShows.TvShow show = null;
            TMDbLib.Objects.Movies.Movie movie = client.GetMovieAsync(id, MovieMethods.Videos).Result;
            
            if (movie != null) //movie exists
            {
                if (movie.Title == null || movie.PosterPath == null || movie.Overview == null) { return null; }
                Console.WriteLine($"Found: {movie.Title}");

                //Get Credits
                var credits = client.GetMovieCreditsAsync(id).Result;
                var people = new List<Cast>();
                foreach (var person in credits.Cast)
                {
                    if(person != null && person.ProfilePath != null)
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
            else 
            {
                show = client.GetTvShowAsync(id, TMDbLib.Objects.TvShows.TvShowMethods.Videos).Result;

                if (show == null) // both movie and show are null
                {
                    throw new InvalidOperationException($"Cant find a movie or show with ID = {id}");
                }

                //Get Credits
                var credits = client.GetTvShowCreditsAsync(id).Result;
                var people = new List<Cast>();
                foreach (var person in credits.Cast)
                {
                    if (person != null && person.ProfilePath != null) //dont save null images
                    {
                        Cast personMovieCast = new Cast()
                        {
                            Id = person.Id,
                            Name = person.Name,
                            ProfilePath = person.ProfilePath,
                        };

                        people.Add(personMovieCast);
                    }
                }

                var movieModel = new MovieCardViewModel()
                {
                    Movie = movie,
                    MovieActorsList = people
                };
                return movieModel;
            }
        }


        public static ShowCardViewModel SearchApiWithShowID(int id)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            var show = client.GetTvShowAsync(id, TMDbLib.Objects.TvShows.TvShowMethods.Videos).Result;

            if (show == null) 
            {
                throw new InvalidOperationException($"Cant find a show with ID = {id}");
            }
            //Get Credits
            var credits = client.GetTvShowCreditsAsync(id).Result;
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
        public static ActorViewModel GetActorWithID(int id)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);
            var actor = client.GetPersonAsync(id).Result;
            var credits = client.GetPersonMovieCreditsAsync(id).Result;
            if(credits == null) //show
            {
                var creditsShow = client.GetPersonTvCreditsAsync(id).Result;

                var model = new ActorViewModel()
                {
                    Person = actor,
                    TvCredits = creditsShow,
                    PhotoUrl = SaveMovieToDbObject.BuildImageURL(actor.ProfilePath),
                };

                return model;
            }
            else //movie
            {
                var model = new ActorViewModel()
                {
                    Person = actor,
                    MovieCredits = credits,
                    PhotoUrl = SaveMovieToDbObject.BuildImageURL(actor.ProfilePath),
                };

                return model;
            }
        }





        public static async Task SearchMovieVideos(string KEY, string Movie_Id)
        {
            TMDbClient client = new TMDbClient(KEY);

            TMDbLib.Objects.Movies.Movie movie = await client.GetMovieAsync(Movie_Id, MovieMethods.Videos);

            Console.WriteLine($"Movie title: {movie.Title}");

            foreach (Video video in movie.Videos.Results)
            {
                Console.WriteLine($"Trailer: {video.Type} ({video.Site}), {video.Name}");
            }
        }
        public static bool IsCorrectTableType(string TableTypeInput)
        {
            string[] types = new[] { "watched", "current", "future" };
            if (types.Contains(TableTypeInput)) { return true; }
            return false;
        }
    }
}
