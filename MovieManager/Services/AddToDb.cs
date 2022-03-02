using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;
using System.Text;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class AddToDb
    {
        private readonly MovieContext data;

        public AddToDb(MovieContext data)
        {
            this.data = data;
        }

        public static string AddMovies(SearchContainer<SearchMovie> results)
        {
            var data = new MovieContext();

            StringBuilder sb = new StringBuilder();
            List<Movie> validMovies = new List<Movie>();

            foreach (SearchMovie result in results.Results)
            {
                if(data.Movies.Where(i => i.MovieId == result.Id).Any())
                {
                    Console.WriteLine($"{result.Title} is already added to watched movies.");
                    continue; // check so nothing is added twice
                }
                var m = SaveMovieToDbObject.MovieApiToObject(result);

                if(m != null)
                {
                    validMovies.Add(m);
                }
                sb.AppendLine($"Successfully added : {result.Title} to watched movies!");
            }

            data.Movies.AddRange(validMovies);
            data.SaveChanges();
            data.Dispose();

            Console.WriteLine(sb.ToString());
            return sb.ToString().Trim();
        }
        

        public static string AddShows(SearchContainer<SearchTv> results)
        {
            var data = new MovieContext();

            StringBuilder sb = new StringBuilder();
            List<Movie> validShows = new List<Movie>();

            foreach (SearchTv result in results.Results)
            {
                if(data.Movies.Where(i => i.MovieId == result.Id).Any()) 
                {
                    Console.WriteLine($"{result.Name} is already added to movies.");
                    continue;
                }

                var m = SaveMovieToDbObject.ShowApiToObject(result);

                if(m != null) { validShows.Add(m); }

                sb.AppendLine($"Successfully added : {result.Name} to show watchlist.");
            }

            data.Movies.AddRange(validShows);
            data.SaveChanges();
            data.Dispose();

            Console.WriteLine(sb.ToString());
            return sb.ToString().Trim();
        }


        public static string AddMovie(SearchMovie movie)
        {
            var data = new MovieContext();

            StringBuilder sb = new StringBuilder();
            Console.WriteLine(movie.Title);

            if (data.Movies.Any(i => i.MovieId == movie.Id))
            {
                sb.Append($"{movie.Title} is already added to movies.");
            }
            else
            {
                var m = SaveMovieToDbObject.MovieApiToObject(movie);

                sb.Append($"Successfully added : {movie.Title} to movies!");

                data.Movies.Add(m);
                data.SaveChanges();
            }
            data.Dispose();

            return sb.ToString();
        }    
    }
}
