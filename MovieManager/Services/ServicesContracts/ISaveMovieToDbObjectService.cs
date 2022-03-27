using MovieManager.Data.DataModels;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISaveMovieToDbObjectService
    {
        Movie SearchMovieApiToObject(SearchMovie result);

        Movie SearchShowApiToObject(SearchTv result);

        Movie MovieApiToObject(TMDbLib.Objects.Movies.Movie result);

        Movie ShowApiToObject(TvShow result);

        Actor ActorApiToObject(TMDbLib.Objects.People.Person result);
    }
}
