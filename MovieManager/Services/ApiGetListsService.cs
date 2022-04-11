using MovieManager.Data.DBConfig;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Client;
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
        public async Task<List<SearchMovie>> GetPopularMovies(int movieCount)
        {
            var results = await tmdbClient.GetMoviePopularListAsync(null, 1, null);

            var returnList = new List<SearchMovie>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var movie in results.Results.Take(movieCount))
            {
                if (movie != null)
                {
                    returnList.Add(movie);
                }
                Console.WriteLine($"Movie title - {movie.Title}");
            }
            return returnList;
        }
        public async Task<List<SearchTv>> GetPopularShows(int showCount)
        {
            var results = await tmdbClient.GetTvShowPopularAsync(1, null);

            var returnList = new List<SearchTv>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var show in results.Results
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
        public async Task<List<SearchMovie>> GetMovieReleases(int movieCount)
        {
            var results = await tmdbClient.GetMovieNowPlayingListAsync(null, 1);

            var returnList = new List<SearchMovie>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var movie in results.Results.Take(movieCount))
            {
                if (movie != null)
                {
                    returnList.Add(movie);
                }
                Console.WriteLine($"Movie title - {movie.Title}");
            }
            return returnList;
        }
        public async Task<List<SearchTv>> GetShowReleases(int showCount)
        {
            var results = await tmdbClient.GetTvShowListAsync(TMDbLib.Objects.TvShows.TvShowListType.AiringToday, 1, null); //.OnTheAir??

            var returnList = new List<SearchTv>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var show in results.Results
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
        public async Task<List<SearchMovie>> GetMovieTrending(int movieCount)
        {
            var results = await tmdbClient.GetTrendingMoviesAsync(TMDbLib.Objects.Trending.TimeWindow.Week, 1);

            var returnList = new List<SearchMovie>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var movie in results.Results.Take(movieCount))
            {
                if (movie != null)
                {
                    returnList.Add(movie);
                }
                Console.WriteLine($"Movie title - {movie.Title}");
            }
            return returnList;
        }
        public async Task<List<SearchTv>> GetShowTrending(int showCount)
        {
            var results = await tmdbClient.GetTvShowListAsync(TMDbLib.Objects.TvShows.TvShowListType.OnTheAir, 1);

            var returnList = new List<SearchTv>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var show in results.Results
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
        public async Task<List<SearchMovie>> GetMovieTopRated(int movieCount)
        {
            var results = await tmdbClient.GetMovieTopRatedListAsync(null, 1, null);

            var returnList = new List<SearchMovie>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var movie in results.Results.Take(movieCount))
            {
                if (movie != null)
                {
                    returnList.Add(movie);
                }
                Console.WriteLine($"Movie title - {movie.Title}");
            }
            return returnList;
        }
        public async Task<List<SearchTv>> GetShowTopRated(int showCount)
        {
            var results = await tmdbClient.GetTvShowTopRatedAsync(1, null);

            var returnList = new List<SearchTv>();

            Console.WriteLine($"Got {results.Results.Count.ToString():N0} of {results.TotalResults.ToString():N0} results");

            foreach (var show in results.Results
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