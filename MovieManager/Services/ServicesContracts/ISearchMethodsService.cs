using MovieManager.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISearchMethodsService
    {
        Task<List<Data.DataModels.Movie>> SearchMovieTitleToList(string SEARCH_NAME);

        Task<List<Data.DataModels.Movie>> SearchShowTitleToList(string SEARCH_NAME);

        Task<MovieCardViewModel> SearchApiWithMovieID(int id);

        Task<ShowCardViewModel> SearchApiWithShowID(int id);

        Task<ActorViewModel> GetActorWithID(int id);

        Task<List<ReviewBase>> GetReviewWithMovieID(int id);

        Task<List<ReviewBase>> GetReviewWithShowID(int id);


        //Trailers
        Task<string> GetMovieTrailer(int id);
        Task<string> GetShowTrailer(int id);
    }
}
