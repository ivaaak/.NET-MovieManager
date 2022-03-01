using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.Contracts
{
    public interface IAddToDb
    {
        abstract string AddMovies(SearchContainer<SearchMovie> results);

        abstract string AddShows(SearchContainer<SearchTv> results);

        abstract string AddMovie(SearchMovie movie);
    }
}
