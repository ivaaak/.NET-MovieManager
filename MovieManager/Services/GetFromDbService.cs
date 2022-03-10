using Microsoft.EntityFrameworkCore;
using MovieManager.Data.DBConfig;
using MovieManager.Data.DataModels;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Services
{
    public class GetFromDbService //:IGetFromDb uncomment after removing static methods
    {
        //needs context injected only?
        private readonly MovieContext dataContext;

        public GetFromDbService() { } //used for DI

        public GetFromDbService(MovieContext data)
        {
            this.dataContext = data;
        }

        public Movie GetMovieFromDBbyID(int MovieId, int i)
        {
			Movie? result = dataContext.Movies.Where(m => m.MovieId == MovieId).FirstOrDefault();

            dataContext.Dispose();

            return result;
        }


        public Movie GetMovieFromDBbyTitle(string MovieTitle)
        {
            var result = dataContext.Movies.Where(m => m.Title.Equals(MovieTitle)).FirstOrDefault();

            dataContext.Dispose();

            return result;
        }


        public List<Movie> GetListFromDBbyTitle(string MovieTitle, int i) 
            //int added to debug/recognize static method in views
        {
            var result = dataContext.Movies.Where(m => m.Title.Contains(MovieTitle)).ToList();

            dataContext.Dispose();

            return result;
        }


        public List<Movie> GetUserMovieListObjects(string UserId, string ListType)
        {
            List<Movie> userMovieObjectsList = new List<Movie>();

            if (ListType.ToLower() == "watched")
            {
                var result = dataContext.Playlists.Where(u => u.User.Id == UserId);
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
            dataContext.Dispose();
            return userMovieObjectsList;
        }

        //GET TRAILER FROM API



        //========= FOR VIEWS DEBUG, REMOVE LATER  ==========
        //this is static for testing atm - calling in views
        //change after discover/popular/releases and main views are switched to viewmodels
        public static List<Movie> GetUserMovieList(string UserId, string listName)
        {
            MovieContext context = new MovieContext();

            var result = context.Playlists
                .Include(a => a.Movies)
                .Where(u => u.User.Id == UserId && u.PlaylistName == listName) //playlistid = listname
                .FirstOrDefault();


            if (result == null) { return null; }

            Console.WriteLine(result.Movies.Count);
            foreach (var item in result.Movies)
            {
                Console.WriteLine($"Movie: {item.Title}");
            }
            return result.Movies;
        }
        public static List<Movie> GetListFromDBbyTitle(string MovieTitle)
        {
            MovieContext context = new MovieContext();

            var result = context.Movies.Where(m => m.Title.Contains(MovieTitle)).ToList();

            context.Dispose();

            return result;
        }
        public static Movie GetMovieFromDBbyID(int MovieId)
        {
            MovieContext context = new MovieContext();

            Movie? result = context.Movies.Where(m => m.MovieId == MovieId).FirstOrDefault();

            context.Dispose();

            return result;
        }
    }
}
