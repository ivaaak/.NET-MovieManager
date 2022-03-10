using MovieManager.Services.ServicesContracts;
using MovieManager.Data.DBConfig;
using MovieManager.Data.DataModels;

namespace MovieManager.Services
{
    public class DeleteMethodsService : IDeleteMethodsService
    {
        private readonly MovieContext dataContext;

        public DeleteMethodsService(MovieContext data)
        {
            this.dataContext = data;
        }

        public string DeleteFromDbUsingName(string movieTitle) //dont use due to cascade deletes in user lists
        {
            if (dataContext.Movies.Any(i => i.Title == movieTitle))
            {
                Console.WriteLine($"Found movie: {movieTitle}. Deleting from Db.");

                var movie = dataContext.Movies.Where(x => x.Title == movieTitle).FirstOrDefault();

                if (movie != null) 
                {
                    dataContext.Movies.Remove(movie);
                    dataContext.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine($"{movieTitle} doesn't exist in the DB!");
            }
            dataContext.Dispose();

            return $"Deleted {movieTitle} from Movies DB.";
        }


        public string DeleteFromDbUsingId(int Id)
        {
            if (dataContext.Movies.Any(i => i.MovieId == Id))
            {
				Movie movie = dataContext.Movies.Where(x => x.MovieId == Id).FirstOrDefault();
				Console.WriteLine($"Found movie with ID: {movie.MovieId} and Name: {movie.Title}. Deleting from Db.");

                dataContext.Movies.Remove(movie);
                dataContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"{Id} doesn't exist in the DB!");
            }
            dataContext.Dispose();
            return $"Deleted Movie with ID - {Id} from Movies DB.";
        }
    }
}
