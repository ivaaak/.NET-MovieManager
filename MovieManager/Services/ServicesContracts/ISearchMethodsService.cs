using MovieManager.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISearchMethodsService
    {
        List<Data.DataModels.Movie> SearchResultCombined(string SEARCH_NAME);

        List<Data.DataModels.Movie> SearchMovieTitleToList(string SEARCH_NAME);

        List<Data.DataModels.Movie> SearchShowTitleToList(string SEARCH_NAME);

        MovieCardViewModel SearchApiWithMovieID(int id);

        ShowCardViewModel SearchApiWithShowID(int id);

        ActorViewModel GetActorWithID(int id);

        List<ReviewBase> GetReviewWithMovieID(int id);

        //save to Db
        void SearchMovieTitleAndSaveToDb(string SEARCH_NAME, AddToDbService addToDb);

        void SearchShowTitleAndSaveToDb(string SEARCH_NAME, AddToDbService addToDb);

        //Trailers
        string GetMovieTrailer(int id);
        string GetShowTrailer(int id);
    }
}
