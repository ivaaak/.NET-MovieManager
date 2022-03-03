using MovieManager.Services.ServicesContracts;
using MovieManager.Data.DBConfig;
using MovieManager.Data.DataModels;

namespace MovieManager.Services
{
    public class DeleteMethods : IDeleteMethods
    {
        private readonly MovieContext data;

        public DeleteMethods(MovieContext data)
        {
            this.data = data;
        }

        public string DeleteFromDbUsingName(string movieTitle) //dont use due to cascade deletes in user lists
        {
            if (data.Movies.Any(i => i.Title == movieTitle))
            {
                Console.WriteLine($"Found movie: {movieTitle}. Deleting from Db.");

                var movie = data.Movies.Where(x => x.Title == movieTitle).FirstOrDefault();

                if (movie != null) 
                {
                    data.Movies.Remove(movie);
                    data.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine($"{movieTitle} doesn't exist in the DB!");
            }
            data.Dispose();

            return $"Deleted {movieTitle} from Movies DB.";
        }


        public string DeleteFromDbUsingId(int Id)
        {
            if (data.Movies.Any(i => i.MovieId == Id))
            {
				Movie movie = data.Movies.Where(x => x.MovieId == Id).FirstOrDefault();
				Console.WriteLine($"Found movie with ID: {movie.MovieId} and Name: {movie.Title}. Deleting from Db.");

                data.Movies.Remove(movie);
                data.SaveChanges();
            }
            else
            {
                Console.WriteLine($"{Id} doesn't exist in the DB!");
            }
            data.Dispose();
            return $"Deleted Movie with ID - {Id} from Movies DB.";
        }
    }
}
