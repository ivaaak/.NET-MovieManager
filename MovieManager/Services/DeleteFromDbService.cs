using MovieManager.Services.ServicesContracts;
using MovieManager.Data;
using MovieManager.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace MovieManager.Services
{
    public class DeleteFromDbService : IDeleteFromDbService
    {
        private readonly MovieContext dataContext;

        public DeleteFromDbService(MovieContext data)
        {
            this.dataContext = data;
        }


        public void DeleteMovieFromUserPlaylist(int movieId, string playlistName, string userName)
        {
            var movie = dataContext.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();


            var targetPlaylist = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(u => u.User.UserName == userName && u.PlaylistName == playlistName)
                .FirstOrDefault();

            if (targetPlaylist.Movies.Contains(movie))
            {
                targetPlaylist.Movies.Remove(movie);
            }

            dataContext.SaveChanges();

            Console.WriteLine($"Removed Movie {movieId} from user - {userName}'s list: {playlistName}");
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
