using MovieManager.Data.DBConfig;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class ApiGetListsService : IApiGetListsService
    {
        private TMDbClient tmdbClient;

        public ApiGetListsService()
        {
            tmdbClient = new TMDbClient(Configuration.APIKey);
        }


        // POPULAR GET
        public List<SearchMovie> GetPopularMovies(int movieCount)
        {
            Task<SearchContainer<SearchMovie>> results = tmdbClient.GetMoviePopularListAsync(null, 1, null);

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
            Task<SearchContainer<SearchTv>> results = tmdbClient.GetTvShowPopularAsync(1, null);

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



        // NEW RELEASES
        public List<SearchMovie> GetMovieReleases(int movieCount)
        {
            var results = tmdbClient.GetMovieNowPlayingListAsync(null, 1);

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
        public List<SearchTv> GetShowReleases(int showCount)
        {
            Task<SearchContainer<SearchTv>> results = tmdbClient.GetTvShowListAsync(TMDbLib.Objects.TvShows.TvShowListType.AiringToday, 1, null); //.OnTheAir??

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



        // TRENDING
        public List<SearchMovie> GetMovieTrending(int movieCount)
        {
            var results = tmdbClient.GetTrendingMoviesAsync(TMDbLib.Objects.Trending.TimeWindow.Week, 1);

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
        public List<SearchTv> GetShowTrending(int showCount)
        {
            Task<SearchContainer<SearchTv>> results = tmdbClient.GetTvShowListAsync(TMDbLib.Objects.TvShows.TvShowListType.OnTheAir, 1);

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



        // TOP RATED
        public List<SearchMovie> GetMovieTopRated(int movieCount)
        {
            var results = tmdbClient.GetMovieTopRatedListAsync(null, 1, null);

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
        public List<SearchTv> GetShowTopRated(int showCount)
        {
            Task<SearchContainer<SearchTv>> results = tmdbClient.GetTvShowTopRatedAsync(1, null);

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