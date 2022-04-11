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
                .ThenInclude(p => p.Movies)
                .Where(u => u.UserName == UserName)
                .SelectMany(u => u.Playlists)
                .ToList();

            return playlists;
        }
        public List<Playlist> GetAllPublicPlaylists()
        {
            var playlists = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(p => p.IsPublic==true)
                .ToList();

            return playlists;
        }


        public List<Movie> GetUserMovieList(string UserName, string listName)
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

            return result;
        }

        public List<Movie> GetMovieListFromDBbyTitle(string MovieTitle)
        {
            var result = dataContext.Movies
                .Where(m => m.Title.Contains(MovieTitle)).ToList();

            return result;
        }
        public string GetUserIdFromUserName(string userName)
        {
            var result = dataContext.Users
                .Where(m => m.UserName==userName)
                .Select(u=>u.Id)
                .FirstOrDefault();

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
                    };

                    userMovieObjectsList.Add(m);
                }
            }
            return userMovieObjectsList;
        }
        public Dictionary<string, QRCodeObject> GetPlaylistsQRCodes(List<Playlist> playlists)
        {
            var qrCodes = new Dictionary<string, QRCodeObject>();

            foreach (var playlist in playlists)
            {
                var res = dataContext.Playlists
                    .Include(a => a.QrCode)
                    .Where(p => p.PlaylistId == playlist.PlaylistId)
                    .Select(p => p.QrCode)
                    .FirstOrDefault();

                qrCodes.Add(playlist.PlaylistId, res);
            }

            return qrCodes;
        }

        //Actors
        public List<Actor> GetUserActors(string UserName)
        {
            var actors = dataContext.Users
                .Include(u => u.Actors)
                .Where(u => u.UserName == UserName)
                .SelectMany(u => u.Actors)
                .ToList();

            return actors;
        }
        //Reviews
        public List<Review> GetAllUserReviews(string userId)
        {
            var reviews = dataContext.Reviews
                .Where(u => u.UserId == userId)
                .ToList();

            return reviews;
        }

        //GET FOR API
        public async Task<string> GetAllUserPlaylistsAsync(string UserName)
        {
            string result = String.Empty;
            try
            {
                var playlists = await dataContext.Users
                   .Include(u => u.Playlists)
                   .ThenInclude(p => p.Movies)
                   .Where(u => u.UserName == UserName)
                   .SelectMany(u => u.Playlists)
                   .ToListAsync();

                result = playlists.ToString();
            } 
            catch (Exception ex)
            {
                return ex.ToString();
            }
           

            return result;
        }
    }
}
