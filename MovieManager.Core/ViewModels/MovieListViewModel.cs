using MovieManager.Infrastructure.Data.Models;

namespace MovieManager.Core.ViewModels
{
    public class MovieListViewModel
    {
        public MovieListViewModel() { }

        public string MoviesListName { get; set; }

        public List<Movie> MoviesList { get; set; }

        //not used in all views
        public List<Movie> MoviesList2 { get; set; }

        public List<Movie> MoviesList3 { get; set; }
    }
}
