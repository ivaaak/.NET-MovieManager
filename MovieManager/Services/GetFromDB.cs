using Microsoft.EntityFrameworkCore;
using MovieManagerMVC.Data.DBConfig;
using MovieManagerMVC.Models.DataModels;

namespace MovieManagerMVC.Services
{
    public class GetFromDB
    {
        public static Movie GetMovieFromDBbyID(int MovieId)
        {
            MovieContext context = new MovieContext();

			Movie? result = context.Movies.Where(m => m.MovieId == MovieId).FirstOrDefault();

            context.Dispose();

            return result;
        }

        public static Movie GetMovieFromDBbyTitle(string MovieTitle)
        {
            MovieContext context = new MovieContext();

            var result = context.Movies.Where(m => m.Title.Equals(MovieTitle)).FirstOrDefault();

            context.Dispose();

            return result;
        }

        public static List<Movie> GetListFromDBbyTitle(string MovieTitle)
        {
            MovieContext context = new MovieContext();

            var result = context.Movies.Where(m => m.Title.Contains(MovieTitle)).ToList();

            context.Dispose();

            return result;
        }

        //needs fixing
        public static List<Movie> GetUserMovieList(string UserId, string listName)
        {
            MovieContext context = new MovieContext();

            var result = context.Playlists
                .Include(a => a.Movies)
                .Where(u => u.UserId == UserId && u.PlaylistId == listName)
                .FirstOrDefault();


            if (result == null) { return null; }

            Console.WriteLine(result.Movies.Count);
            foreach (var item in result.Movies)
            {
                Console.WriteLine($"Movie: {item.Title}");
            }
            return result.Movies;
        }

        public static List<Movie> GetUserMovieListObjects(string UserId, string ListType)
        {
            MovieContext context = new MovieContext();

            List<Movie> userMovieObjectsList = new List<Movie>();

            if (ListType.ToLower() == "watched")
            {
                var result = context.Playlists.Where(u => u.UserId == UserId);
                foreach (var item in result)
                {
                    Movie m = new Movie()
                    {
                        //MovieId = item.MovieId,
                        //Title = context.Movies.Where(n => n.MovieId == item.Id).FirstOrDefault().Title,
                        //PosterUrl = context.Movies.Where(n => n.MovieId == item.Id).FirstOrDefault().PosterUrl,
                        //Overview = context.Movies.Where(n => n.MovieId == item.Id).FirstOrDefault().Overview,
                        //Rating = context.Movies.Where(n => n.MovieId == item.Id).FirstOrDefault().Rating,
                        //ReleaseDate = context.Movies.Where(n => n.MovieId == item.Id).FirstOrDefault().ReleaseDate,
                    };

                    userMovieObjectsList.Add(m);
                }
            }
            context.Dispose();
            return userMovieObjectsList;
        }

        //GET TRAILER FROM API
    }
}
