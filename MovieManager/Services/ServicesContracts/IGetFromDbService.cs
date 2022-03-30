using MovieManager.Data.DataModels;

namespace MovieManager.Services.ServicesContracts
{
    public interface IGetFromDbService
    {
        Movie GetMovieFromDBbyID(int MovieId);

        Movie GetMovieFromDBbyTitle(string MovieTitle);

        List<Movie> GetMovieListFromDBbyTitle(string MovieTitle);

        List<Movie> GetUserMovieListObjects(string UserId, string ListType);
        
        List<Movie> GetUserMovieList(string UserName, string listName);

        List<Playlist> GetAllUserPlaylists(string UserName);

        Task<string> GetAllUserPlaylistsAsync(string UserName);
    }
}
