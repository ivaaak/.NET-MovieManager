using MovieManager.Data.DBConfig;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class ApiGetPopular
    {
        public static List<SearchMovie> GetPopularMovies(int movieCount)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            Task<SearchContainer<SearchMovie>> results = client.GetMoviePopularListAsync();

            var returnList = new List<SearchMovie>();

            Console.WriteLine($"Got {results.Result.Results.Count.ToString():N0} of {results.Result.TotalResults.ToString():N0} results");

            foreach (var movie in results.Result.Results.Take(movieCount))
            {
                if (movie != null)
                {
                    returnList.Add(movie);
                }
                Console.WriteLine($"Movie title - {movie.Title}");
            }
            Console.WriteLine(Environment.NewLine);

            client.Dispose();

            return returnList;
        }


        public static List<SearchTv> GetPopularShows(int showCount)
        //search and return a Movie list
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            Task<SearchContainer<SearchTv>> results = client.GetTvShowPopularAsync();

            var returnList = new List<SearchTv>();

            Console.WriteLine($"Got {results.Result.Results.Count.ToString():N0} of {results.Result.TotalResults.ToString():N0} results");

            foreach (var show in results.Result.Results
                .Where(x => x.PosterPath != null)
                .OrderByDescending(x => x.Popularity)
                .Take(showCount))
            {
                returnList.Add(show);

                Console.WriteLine($"Show name - {show.Name}");
            }

            client.Dispose();

            return returnList;
        }
    }
}
