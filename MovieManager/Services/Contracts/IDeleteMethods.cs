using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.Contracts
{
    public interface IDeleteMethods
    {
        string DeleteFromDbUsingName(string movieTitle);

        string DeleteFromDbUsingId(int  movieId);
    }
}
