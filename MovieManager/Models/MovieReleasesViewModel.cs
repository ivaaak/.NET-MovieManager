using MovieManager.Data.DataModels;
using TMDbLib.Objects.Movies;

namespace MovieManager.Models
{
    public class MovieReleasesViewModel
    {
        public MovieReleasesViewModel() { }

        public List<TMDbLib.Objects.Movies.Movie> MoviesList { get; set; }
        
        public Review MovieReview { get; set; }
        //reviews?
    }
}
