using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IAddToDbService
    {
        string AddMovies(SearchContainer<SearchMovie> results);

        string AddShows(SearchContainer<SearchTv> results);

        string AddMovie(SearchMovie movie);
    }
}
