using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.DataModels;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Services
{
    public class GetFromDbService : IGetFromDbService
    {
        private readonly MovieContext dataContext;

        public GetFromDbService() { } 

        public GetFromDbService(MovieContext data)
        {
            this.dataContext = data;
        }

        public List<Playlist> GetAllUserPlaylists(string UserName)
        {
            var playlists = dataContext.Users
                .Include(u => u.Playlists)
                .Where(u => u.UserName == UserName)
                .SelectMany(u => u.Playlists)
                .ToList();

            return playlists;
        }
        

        public List<Movie> GetUserMovieList(string UserName, string listName) //used to be Id but this.User.Identity doesnt access Id
        {
            var result = dataContext.Playlists
                .Include(a => a.Movies)
                .Where(u => u.User.UserName == UserName && u.PlaylistName == listName) 
                .FirstOrDefault();

            if (result == null) { return null; }

            Console.WriteLine($"The number of movies in playlist {result.PlaylistName} are {result.Movies.Count}");

            return result.Movies;
        }


        public Movie GetMovieFromDBbyID(int MovieId)
        {
            Movie? result = dataContext.Movies
                .Where(m => m.MovieId == MovieId).FirstOrDefault();

            return result;
        }

        public Movie GetMovieFromDBbyTitle(string MovieTitle)
        {
            var result = dataContext.Movies
                .Where(m => m.Title.Equals(MovieTitle)).FirstOrDefault(); 
                //change equals to contains?

            return result;
        }

        public List<Movie> GetMovieListFromDBbyTitle(string MovieTitle)
        {
            var result = dataContext.Movies
                .Where(m => m.Title.Contains(MovieTitle)).ToList();

            return result;
        }


        //WIP
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
                    };

                    userMovieObjectsList.Add(m);
                }
            }
            return userMovieObjectsList;
        }

        //GET TRAILER FROM API/DB

    }
}
