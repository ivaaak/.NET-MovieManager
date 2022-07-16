using MovieManager.Core.ViewModels;
using MovieManager.Infrastructure.Data.Models;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Services.ServicesContracts
{
    public interface ISearchMethodsService
    {
        Task<List<Movie>> SearchMovieTitleToList(string SEARCH_NAME);

        Task<List<Movie>> SearchShowTitleToList(string SEARCH_NAME);

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
