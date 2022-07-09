using TMDbLib.Objects.Search;

namespace MovieManager.Core.ViewModels
{
    public class MovieDiscoverViewModel
    {
        public MovieDiscoverViewModel() { }

        public List<SearchMovie> DiscoverMovies { get; set; }

        public List<SearchTv> DiscoverShows { get; set; }
    }
}
