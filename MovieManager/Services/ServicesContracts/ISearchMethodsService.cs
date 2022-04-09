using MovieManager.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISearchMethodsService
    {
        List<Data.DataModels.Movie> SearchMovieTitleToList(string SEARCH_NAME);

        List<Data.DataModels.Movie> SearchShowTitleToList(string SEARCH_NAME);

        MovieCardViewModel SearchApiWithMovieID(int id);

        ShowCardViewModel SearchApiWithShowID(int id);

        ActorViewModel GetActorWithID(int id);

        List<ReviewBase> GetReviewWithMovieID(int id);

        List<ReviewBase> GetReviewWithShowID(int id);


        //Trailers
        string GetMovieTrailer(int id);
        string GetShowTrailer(int id);
    }
}
