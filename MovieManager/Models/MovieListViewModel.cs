using MovieManager.Data.DataModels;

namespace MovieManager.Models
{
    public class MovieListViewModel
    {
        public MovieListViewModel() { }

        public string MoviesListName { get; set; }

        public List<Movie> MoviesList { get; set; }
        //Playlist instead of a movie list?

        public List<Movie> MoviesList2 { get; set; }

        public List<Movie> MoviesList3 { get; set; }
    }
}
