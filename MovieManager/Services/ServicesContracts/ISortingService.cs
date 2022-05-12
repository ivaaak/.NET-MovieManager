namespace MovieManager.Services.ServicesContracts
{
    public interface ISortingService
    {
        Task SortPlaylistByMovieTitle(string userName, string playlistName);

        Task SortPlaylistByMovieRating(string userName, string playlistName);

        Task SortPlaylistByMovieDateAscending(string userName, string playlistName);

        Task SortPlaylistByMovieDateDescending(string userName, string playlistName);

        Task SortActorsByName(string UserName);

        Task SortUserReviewsByRating(string userId);

        Task SortUserReviewsByTitle(string userId);
    }
}
