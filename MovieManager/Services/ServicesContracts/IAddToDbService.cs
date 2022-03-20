using MovieManager.Data.DataModels;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IAddToDbService
    {
        void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name);

        //void AddShowToUserPlaylist(SearchTv show, string PlaylistName, string Name);

        string AddMovies(SearchContainer<SearchMovie> results);

        string AddShows(SearchContainer<SearchTv> results);

        string AddMovie(SearchMovie movie);
    }
}
