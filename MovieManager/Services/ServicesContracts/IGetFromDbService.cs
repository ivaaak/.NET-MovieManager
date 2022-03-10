using MovieManager.Data.DataModels;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IGetFromDbService
    {
        Movie GetMovieFromDBbyID(int MovieId);

        Movie GetMovieFromDBbyTitle(string MovieTitle);

        List<Movie> GetListFromDBbyTitle(string MovieTitle);

        List<Movie> GetUserMovieListObjects(string UserId, string ListType);
    }
}
