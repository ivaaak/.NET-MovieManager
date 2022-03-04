using MovieManager.Data.DBConfig;
using MovieManager.Data.DataModels;
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
            //return show and movie results in a single list
        }

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
        //search and add to DB
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchMovie> results = client.SearchMovieAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            AddToDb.AddMovies(results);

            client.Dispose();
        }


        public static void SearchShowTitleAndSaveToDb(string SEARCH_NAME)
        //search and add to DB
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchTv> results = client.SearchTvShowAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            AddToDb.AddShows(results);

            client.Dispose();
        }




        public static Data.DataModels.Movie SearchApiWithID(int id)
            //should also take a string MediaType to differentiate between show and movie
            //search based on Id and Media type
            //(id 500 can be a show or movie)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);
            TMDbLib.Objects.TvShows.TvShow show = null;
            TMDbLib.Objects.Movies.Movie movie = client.GetMovieAsync(id, MovieMethods.Videos).Result;

            if(movie != null) //movie exists
            {
                if (movie.Title == null || movie.PosterPath == null || movie.Overview == null) { return null; }
                Console.WriteLine($"Found: {movie.Title}");

                var movieDbObj = new Data.DataModels.Movie //movie obj from DB
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    PosterUrl = SaveMovieToDbObject.BuildImageURL(movie.PosterPath),
                    Overview = movie.Overview,
                    ReleaseDate = movie.ReleaseDate,
                    Popularity = (decimal)movie.Popularity,
                    Rating = (decimal)movie.VoteAverage,
                    //Genre = movie.Genres.ToString(),
                    //LanguageId = movie.OriginalLanguage, 

                };
                return movieDbObj;
            }
            else 
            {
                show = client.GetTvShowAsync(id, TMDbLib.Objects.TvShows.TvShowMethods.Videos).Result;

                if (show == null) // both movie and show are null
                {
                    throw new InvalidOperationException($"Cant find a movie or show with ID = {id}");
                }

                var movieDbObj = new Data.DataModels.Movie //movie obj from DB
                {
                    MovieId = show.Id,
                    Title = show.Name,
                    PosterUrl = SaveMovieToDbObject.BuildImageURL(show.PosterPath),
                    Overview = show.Overview,
                    ReleaseDate = show.FirstAirDate,
                    Popularity = (decimal)show.Popularity,
                    Rating = (decimal)show.VoteAverage,
                };
                return movieDbObj;
            }
        }







        //TODO
        public static async Task SearchMovieCast(string KEY, string Movie_Id)
        {
            TMDbClient client = new TMDbClient(KEY);

            //TODO write result to the json.txt - INIT writer here
            //var jsonSavePath = "D:\\Softuni\\WEB PROJ IDEA\\MOVI\\Backend C# EF\\MovieManager\\Movies\\JSONstring.txt";
            //await using StreamWriter jsonWriter = File.AppendText(jsonSavePath);

            TMDbLib.Objects.Movies.Movie movie = await client.GetMovieAsync(Movie_Id, MovieMethods.Credits);

            Console.WriteLine($"Movie title: {movie.Title}");
            //await jsonWriter.WriteLineAsync($"Movie title: {movie.Title}");

            foreach (Cast cast in movie.Credits.Cast)
            {
                Console.WriteLine($"{cast.Name} - {cast.Character}");
                //await jsonWriter.WriteLineAsync($"    {cast.Name} - {cast.Character}");
                //Add to Actors DB Table
            }
        }



        public static async Task SearchMovieVideos(string KEY, string Movie_Id)
        {
            TMDbClient client = new TMDbClient(KEY);

            var jsonSavePath = "D:\\Softuni\\WEB PROJ IDEA\\MOVI\\Backend C# EF\\MovieManager\\Movies\\JSONstring.txt";
            await using StreamWriter jsonWriter = File.AppendText(jsonSavePath);

            TMDbLib.Objects.Movies.Movie movie = await client.GetMovieAsync(Movie_Id, MovieMethods.Videos);

            Console.WriteLine($"Movie title: {movie.Title}");

            foreach (Video video in movie.Videos.Results)
            {
                Console.WriteLine($"Trailer: {video.Type} ({video.Site}), {video.Name}");
                await jsonWriter.WriteLineAsync($"Trailer: {video.Type} ({video.Site}), {video.Name}");
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
