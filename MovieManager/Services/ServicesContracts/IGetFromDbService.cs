using MovieManager.Data.DataModels;

namespace MovieManager.Services.ServicesContracts
{
    public interface IGetFromDbService
    {
        Task<List<Playlist>> GetAllUserPlaylists(string UserName);

        Task<List<Playlist>> GetAllPublicPlaylists();


        Task<Movie> GetMovieFromDBbyID(int MovieId);

        Task<Movie> GetMovieFromDBbyTitle(string MovieTitle);


        Task<List<Movie>> GetMovieListFromDBbyTitle(string MovieTitle);

        Task<List<Movie>> GetUserMovieList(string UserName, string listName);

        Task<List<Actor>> GetUserActors(string UserName);

        Task<List<Review>> GetAllUserReviews(string userId);

        Task<string> GetUserIdFromUserName(string userName);

        Task<Dictionary<string, QRCodeObject>> GetPlaylistsQRCodes(List<Playlist> playlists);



        //api
        Task<string> GetAllUserPlaylistsAsync(string UserName);
    }
}
