using MovieManager.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISearchMethodsService
    {
        List<Data.DataModels.Movie> SearchResultCombined(string SEARCH_NAME);

        List<Data.DataModels.Movie> SearchMovieTitleToList(string SEARCH_NAME);

        List<Data.DataModels.Movie> SearchShowTitleToList(string SEARCH_NAME);

        //save to Db
        void SearchMovieTitleAndSaveToDb(string SEARCH_NAME, AddToDbService addToDb);

        void SearchShowTitleAndSaveToDb(string SEARCH_NAME, AddToDbService addToDb);

        MovieCardViewModel SearchApiWithMovieID(int id);

        ShowCardViewModel SearchApiWithShowID(int id);

        ActorViewModel GetActorWithID(int id);

        Task SearchMovieVideos(string KEY, string Movie_Id);

        bool IsCorrectTableType(string TableTypeInput);
    }
}
