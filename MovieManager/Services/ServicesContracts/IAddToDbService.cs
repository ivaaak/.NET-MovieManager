using MovieManager.Models;

namespace MovieManager.Services.ServicesContracts
{
    public interface IAddToDbService
    {
        void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddShowToUserPlaylist(int movieId, string PlaylistName, string Name);

        Task AddMovieToFavorites(int movieId, string Name);

        void AddActorToUserList(int ActorId, string Name);

        Task AddReviewToUsersReviews(string reviewTitle, string reviewContent, decimal rating, string userName, int movieId, string movieTitle);

        Task GenerateQRCode(string playlistId);

        Task CreateCustomPlaylist(string playlistTitle, string userId);
    }
}
