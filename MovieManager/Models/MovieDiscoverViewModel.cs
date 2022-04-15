using TMDbLib.Objects.Search;

namespace MovieManager.Models
{
    public class MovieDiscoverViewModel
    {
        public MovieDiscoverViewModel() { }

        public List<SearchMovie> DiscoverMovies { get; set; }

        public List<SearchTv> DiscoverShows { get; set; }
    }
}
