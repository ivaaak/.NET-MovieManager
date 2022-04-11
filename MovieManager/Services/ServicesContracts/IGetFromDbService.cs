using MovieManager.Data.DataModels;

namespace MovieManager.Services.ServicesContracts
{
    public interface IGetFromDbService
    {
        List<Playlist> GetAllUserPlaylists(string UserName);

        List<Playlist> GetAllPublicPlaylists();


        Movie GetMovieFromDBbyID(int MovieId);

        Movie GetMovieFromDBbyTitle(string MovieTitle);


        List<Movie> GetMovieListFromDBbyTitle(string MovieTitle);

        List<Movie> GetUserMovieListObjects(string UserId, string ListType);
        
        List<Movie> GetUserMovieList(string UserName, string listName);

        List<Actor> GetUserActors(string UserName);

        List<Review> GetAllUserReviews(string userId);

        string GetUserIdFromUserName(string userName);

        Dictionary<string, QRCodeObject> GetPlaylistsQRCodes(List<Playlist> playlists);



        //api
        Task<string> GetAllUserPlaylistsAsync(string UserName);
    }
}
