using TMDbLib.Objects.Search;

namespace MovieManager.Core.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public List<SearchMovie> DiscoverMovies { get; set; }

        public List<SearchTv> DiscoverShows { get; set; }
    }
}
