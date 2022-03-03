using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IDeleteMethods
    {
        string DeleteFromDbUsingName(string movieTitle);

        string DeleteFromDbUsingId(int  movieId);
    }
}
