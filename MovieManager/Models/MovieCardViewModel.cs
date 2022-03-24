using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;

namespace MovieManager.Models
{
    public class MovieCardViewModel
    {
        public MovieCardViewModel() { }

        public Movie Movie { get; set; }

        public List<Cast> MovieActorsList { get; set; }

        public List<ReviewBase> Reviews { get; set; }
    }
}
