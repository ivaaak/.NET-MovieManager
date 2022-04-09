using MovieManager.Data.DBConfig;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class PrintApiObject	//this class is for testing purposes only
    {
        public static void ApiWrapperTestCall(string KEY, string SEARCH_NAME)
		{
			PrintMovieObj("");
		}


		public static void PrintMovieObj(string SEARCH_NAME)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchMovie> results = client.SearchMovieAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            foreach (SearchMovie result in results.Results)
            {
                Console.WriteLine($"Title: {result.Title}");
                Console.WriteLine($"Adult: {result.Adult}");
                Console.WriteLine($"Backdrop Path: {result.BackdropPath}");
                Console.WriteLine($"GenreIds: {String.Join(" ", result.GenreIds)}");
                Console.WriteLine($"MediaType: {result.MediaType}");
                Console.WriteLine($"OriginalLanguage: {result.OriginalLanguage}");
                Console.WriteLine($"OriginalTitle: {result.OriginalTitle}");
                Console.WriteLine($"Popularity: {result.Popularity}");
                Console.WriteLine($"PosterPath: {result.PosterPath}");
                Console.WriteLine($"ReleaseDate: {result.ReleaseDate}");
                Console.WriteLine($"Video: {result.Video}");
                Console.WriteLine($"VoteAverage: {result.VoteAverage}");
                Console.WriteLine($"VoteCount: {result.VoteCount}");
            }
        }
        public static void PrintShowObj(string SEARCH_NAME)
        {
            TMDbClient client = new TMDbClient(Configuration.APIKey);

            SearchContainer<SearchTv> results = client.SearchTvShowAsync(SEARCH_NAME).Result;

            Console.WriteLine($"Got {results.Results.Count:N0} of {results.TotalResults:N0} results");

            foreach (SearchTv result in results.Results)
            {
                Console.WriteLine($"Title: {result.Name}");
                Console.WriteLine($"Country: {result.OriginCountry}");
                Console.WriteLine($"Backdrop Path: {result.BackdropPath}");
                Console.WriteLine($"GenreIds: {String.Join(" ", result.GenreIds)}");
                Console.WriteLine($"MediaType: {result.MediaType}");
                Console.WriteLine($"OriginalLanguage: {result.OriginalLanguage}");
                Console.WriteLine($"Popularity: {result.Popularity}");
                Console.WriteLine($"PosterPath: {result.PosterPath}");
                Console.WriteLine($"FirstAirDate: {result.FirstAirDate}");
                Console.WriteLine($"VoteAverage: {result.VoteAverage}");
                Console.WriteLine($"VoteCount: {result.VoteCount}");
            }
        }
    }
}
