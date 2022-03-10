using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;
using MovieManager.Services.ServicesContracts;
using System.Text;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieManager.Services
{
    public class AddToDbService : IAddToDbService
    {
        private readonly MovieContext dataContext;

        private ISaveMovieToDbObjectService service;

        public AddToDbService() { } //used for DI

        public AddToDbService(
            ISaveMovieToDbObjectService saveMovieToDbObjectService, 
            MovieContext data) 
        {
            this.service = saveMovieToDbObjectService;
            this.dataContext = data;
        }


        public string AddMovies(SearchContainer<SearchMovie> results)
        {
            StringBuilder sb = new StringBuilder();
            List<Movie> validMovies = new List<Movie>();

            foreach (SearchMovie result in results.Results)
            {
                if(dataContext.Movies.Where(i => i.MovieId == result.Id).Any())
                {
                    Console.WriteLine($"{result.Title} is already added to watched movies.");
                    continue; // check so nothing is added twice
                }
                var m = this.service.MovieApiToObject(result);

                if(m != null)
                {
                    validMovies.Add(m);
                }
                sb.AppendLine($"Successfully added : {result.Title} to watched movies!");
            }

            dataContext.Movies.AddRange(validMovies);
            dataContext.SaveChanges();
            dataContext.Dispose();

            Console.WriteLine(sb.ToString());
            return sb.ToString().Trim();
        }
        

        public string AddShows(SearchContainer<SearchTv> results)
        {
            StringBuilder sb = new StringBuilder();
            List<Movie> validShows = new List<Movie>();

            foreach (SearchTv result in results.Results)
            {
                if(dataContext.Movies.Where(i => i.MovieId == result.Id).Any()) 
                {
                    Console.WriteLine($"{result.Name} is already added to movies.");
                    continue;
                }

                var m = this.service.ShowApiToObject(result);

                if(m != null) { validShows.Add(m); }

                sb.AppendLine($"Successfully added : {result.Name} to show watchlist.");
            }

            dataContext.Movies.AddRange(validShows);
            dataContext.SaveChanges();
            dataContext.Dispose();

            Console.WriteLine(sb.ToString());
            return sb.ToString().Trim();
        }


        public string AddMovie(SearchMovie movie)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine(movie.Title);

            if (dataContext.Movies.Any(i => i.MovieId == movie.Id))
            {
                sb.Append($"{movie.Title} is already added to movies.");
            }
            else
            {
                var m = this.service.MovieApiToObject(movie);

                sb.Append($"Successfully added : {movie.Title} to movies!");

                dataContext.Movies.Add(m);
                dataContext.SaveChanges();
            }
            dataContext.Dispose();

            return sb.ToString();
        }    
    }
}
