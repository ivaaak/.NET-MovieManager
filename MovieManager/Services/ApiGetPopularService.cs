using MovieManager.Data.DBConfig;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class ApiGetPopularService : IApiGetPopularService
    {
        private TMDbClient tmdbClient;

        public ApiGetPopularService()
        {
            tmdbClient = new TMDbClient(Configuration.APIKey);
        }

        public List<SearchMovie> GetPopularMovies(int movieCount)
        {
            Task<SearchContainer<SearchMovie>> results = tmdbClient.GetMoviePopularListAsync();

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

            return returnList;
        }


        public List<SearchTv> GetPopularShows(int showCount)
        {
            Task<SearchContainer<SearchTv>> results = tmdbClient.GetTvShowPopularAsync();

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

            return returnList;
        }
    }
}
