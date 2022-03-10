using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface IDeleteMethodsService
    {
        string DeleteFromDbUsingName(string movieTitle);

        string DeleteFromDbUsingId(int  movieId);
    }
}
