using MovieManagerMVC.Data.DBConfig;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace MovieManagerMVC.Services
{
    public class SearchMethods
    {
        public static void SearchMovieTitle(string SEARCH_NAME)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchMovie> results = client.SearchMovieAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            AddToDb.AddMovies(results);

            client.Dispose();
        }


        public static void SearchShowTitle(string SEARCH_NAME, string WhichDbToAddTo)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchTv> results = client.SearchTvShowAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            AddToDb.AddShows(results);

            client.Dispose();
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
