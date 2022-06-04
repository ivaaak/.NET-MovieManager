using MovieManager.Data.DataModels;

namespace MovieManager.Services.ServicesContracts
{
    public interface IGetFromDbService
    {
        Task<List<Playlist>> GetAllUserPlaylists(string UserName);

        Task<List<Playlist>> GetAllPublicPlaylists();

        Task<List<Movie>> GetUserMovieList(string UserName, string listName);

        Task<List<Actor>> GetUserActors(string UserName);

        Task<List<Review>> GetAllUserReviews(string userId);

        Task<string> GetUserIdFromUserName(string userName);

        Task<Dictionary<string, QRCodeObject>> GetPlaylistsQRCodes(List<Playlist> playlists);


        Task<Movie> GetMovieDataFromId(int id);

        Task<List<Movie>> GetMovieDataFromName(string movieName);
    }
}
