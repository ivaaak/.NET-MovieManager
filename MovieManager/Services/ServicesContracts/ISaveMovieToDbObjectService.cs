using MovieManager.Data.DataModels;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISaveMovieToDbObjectService
    {
        Movie MovieApiToObject(SearchMovie result);

        Movie ShowApiToObject(SearchTv result);
    }
}
