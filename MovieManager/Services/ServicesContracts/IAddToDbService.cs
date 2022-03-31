using MovieManager.Data.DataModels;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IAddToDbService
    {
        void AddMovieToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddShowToUserPlaylist(int movieId, string PlaylistName, string Name);

        void AddMovieToFavorites(int movieId, string Name);

        void AddActorToUserList(int ActorId, string Name);

        void AddReviewToUsersReviews(Review review, string Name);

        void GenerateQRCode(Playlist playlist);

        //add to db for debug
        void AddMovies(SearchContainer<SearchMovie> results);

        void AddShows(SearchContainer<SearchTv> results);

        void AddMovie(SearchMovie movie);
    }
}
