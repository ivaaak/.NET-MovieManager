using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IDeleteFromDbService
    {
        void DeleteMovieFromUserPlaylist(int movieId, string playlistName, string userName);

        string DeleteFromDbUsingName(string movieTitle);

        string DeleteFromDbUsingId(int  movieId);
    }
}
