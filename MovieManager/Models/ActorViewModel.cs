using TMDbLib.Objects.Movies;
using TMDbLib.Objects.TvShows;

namespace MovieManager.Models
{
    public class IndexViewModel
    {
        public List<Movie> DiscoverMovies { get; set; }

        public List<TvShow> DiscoverShows { get; set; }
    }
}