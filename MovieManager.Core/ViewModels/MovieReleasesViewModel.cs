using MovieManager.Infrastructure.Data.Models;

namespace MovieManager.Core.ViewModels
{
    public class MovieReleasesViewModel
    {
        public MovieReleasesViewModel() { }

        public List<TMDbLib.Objects.Movies.Movie> MoviesList { get; set; }
        
        public Review MovieReview { get; set; }
    }
}
