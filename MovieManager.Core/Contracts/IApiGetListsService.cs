using TMDbLib.Objects.Search;

namespace MovieManager.Core.Contracts
{
    public interface IApiGetListsService
    {
        Task<List<SearchMovie>> GetPopularMovies(int movieCount);
        Task<List<SearchTv>> GetPopularShows(int showCount);

        // RELEASES
        Task<List<SearchMovie>> GetMovieReleases(int movieCount);
        Task<List<SearchTv>> GetShowReleases(int showCount);

        // TRENDING
        Task<List<SearchMovie>> GetMovieTrending(int movieCount);
        Task<List<SearchTv>> GetShowTrending(int showCount);

        // TOP RATED
        Task<List<SearchMovie>> GetMovieTopRated(int movieCount);
        Task<List<SearchTv>> GetShowTopRated(int showCount);
    }
}
