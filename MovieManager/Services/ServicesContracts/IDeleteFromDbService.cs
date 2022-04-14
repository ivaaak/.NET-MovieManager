namespace MovieManager.Services.ServicesContracts
{
    public interface IDeleteFromDbService
    {
        void DeleteMovieFromUserPlaylist(int movieId, string playlistName, string userName);

        void DeleteActorFromUserList(int actorId, string userName);

        Task<string> DeleteFromDbUsingName(string movieTitle);

        Task<string> DeleteFromDbUsingId(int  movieId);

    }
}
