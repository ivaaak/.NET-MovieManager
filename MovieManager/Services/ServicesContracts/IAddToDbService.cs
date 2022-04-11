using MovieManager.Models;

namespace MovieManager.Services.ServicesContracts
{
    public interface IAddToDbService
    {
        void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddShowToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddMovieToFavorites(int movieId, string Name);

        void AddActorToUserList(int ActorId, string Name);

        void AddReviewToUsersReviews(ReviewViewModel model, string userId, string movieId);

        void GenerateQRCode(string playlistId);

        void CreateCustomPlaylist(string playlistTitle, string userId);
    }
}
