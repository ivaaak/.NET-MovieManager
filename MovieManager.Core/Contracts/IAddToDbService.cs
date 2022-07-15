namespace MovieManager.Core.Contracts
{
    public interface IAddToDbService
    {
        void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddShowToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddMovieToFavorites(int movieId, string Name);

        void AddActorToUserList(int ActorId, string Name);

        void AddReviewToUsersReviews(string reviewTitle, string reviewContent, decimal rating, string userName, int movieId, string movieTitle);

        void GenerateQRCode(string playlistId);

        void CreateCustomPlaylist(string playlistTitle, string userId);
    }
}
