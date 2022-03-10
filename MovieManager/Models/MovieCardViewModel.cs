using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;

namespace MovieManager.Models
{
    public class MovieCardViewModel
    {
        public MovieCardViewModel() { }

        public Movie Movie { get; set; }

        public List<Cast> MovieActorsList { get; set; }
    }
}
