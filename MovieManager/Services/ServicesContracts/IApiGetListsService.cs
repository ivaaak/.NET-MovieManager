using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IApiGetListsService
    {
        List<SearchMovie> GetPopularMovies(int movieCount);
        List<SearchTv> GetPopularShows(int showCount);

        // RELEASES
        List<SearchMovie> GetMovieReleases(int movieCount);
        List<SearchTv> GetShowReleases(int showCount);

        // TRENDING
        List<SearchMovie> GetMovieTrending(int movieCount);
        List<SearchTv> GetShowTrending(int showCount);

        // TOP RATED
        List<SearchMovie> GetMovieTopRated(int movieCount);
        List<SearchTv> GetShowTopRated(int showCount);
    }
}
