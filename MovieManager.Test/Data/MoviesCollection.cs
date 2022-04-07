using MovieManager.Data.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.Test.Data
{
    public class MoviesCollection
    {
        public static IEnumerable<Movie> moviesCollection
            => Enumerable.Range(0, 100).Select(i => new Movie
            {
                MediaType = "movie"
            });

        public static IEnumerable<Movie> showsCollection
            => Enumerable.Range(0, 100).Select(i => new Movie
            {
                MediaType = "show"
            });
    }
}
