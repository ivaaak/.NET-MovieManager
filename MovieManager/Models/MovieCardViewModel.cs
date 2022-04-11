using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Models
{
    public class MovieCardViewModel
    {
        public MovieCardViewModel() { }

        public Movie Movie { get; set; }

        public List<Cast> MovieActorsList { get; set; }

        public List<ReviewBase> Reviews { get; set; }

        //for review post
        public string ReviewTitle { get; set; }

        public string ReviewContent { get; set; }

        public decimal Rating { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
    }
}
