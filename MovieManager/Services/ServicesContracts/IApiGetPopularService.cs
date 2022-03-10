using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IApiGetPopularService
    {
        List<SearchMovie> GetPopularMovies(int movieCount);

        List<SearchTv> GetPopularShows(int showCount);

    }
}
