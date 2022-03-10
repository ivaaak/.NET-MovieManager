using MovieManager.Data.DataModels;

namespace MovieManager.Models
{
    public class MovieReleasesViewModel
    {
        public MovieReleasesViewModel() { }

        public List<Movie> MoviesList { get; set; }
        
        public Review MovieReview { get; set; }
        //reviews?
    }
}
