using Microsoft.EntityFrameworkCore;
using MovieManager.Api.ApiServices.Models;

namespace MovieManager.Api.ApiServices
{
    public class GetFromDbService : IGetFromDbService
    {
        private readonly MovieContext dataContext;

        public GetFromDbService() { } 

        public GetFromDbService(MovieContext data)
        {
            this.dataContext = data;
        }

        public async Task<List<Playlist>> GetAllUserPlaylists(string UserName)
        {
            var playlists = await dataContext.Users
                .Include(u => u.Playlists)
                .ThenInclude(p => p.Movies)
                .Where(u => u.UserName == UserName)
                .SelectMany(u => u.Playlists)
                .ToListAsync();

            return playlists;
        }

        public async Task<List<Playlist>> GetAllPublicPlaylists()
        {
            var playlists = await dataContext.Playlists
                .Include(p => p.Movies)
                .Where(p => p.IsPublic == true)
                .ToListAsync();

            return playlists;
        }

        public async Task<List<Movie>> GetUserMovieList(string UserName, string listName)
        {
            var result = await dataContext.Playlists
                .Include(a => a.Movies)
                .Where(u => u.User.UserName == UserName && u.PlaylistName == listName) 
                .FirstOrDefaultAsync();

            if (result == null) { return null; }

            Console.WriteLine($"The number of movies in playlist {result.PlaylistName} are {result.Movies.Count}");

            return result.Movies;
        }

        public async Task<string> GetUserIdFromUserName(string userName)
        {
            var result = await dataContext.Users
                .Where(m => m.UserName==userName)
                .Select(u=>u.Id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Dictionary<string, QRCodeObject>> GetPlaylistsQRCodes(List<Playlist> playlists)
        {
            var qrCodes = new Dictionary<string, QRCodeObject>();

            foreach (var playlist in playlists)
            {
                var res = await dataContext.Playlists
                    .Include(a => a.QrCode)
                    .Where(p => p.PlaylistId == playlist.PlaylistId)
                    .Select(p => p.QrCode)
                    .FirstOrDefaultAsync();

                qrCodes.Add(playlist.PlaylistId, res);
            }

            return qrCodes;
        }

        //Actors
        public async Task<List<Actor>> GetUserActors(string UserName)
        {
            var actors = await dataContext.Users
                .Include(u => u.Actors)
                .Where(u => u.UserName == UserName)
                .SelectMany(u => u.Actors)
                .ToListAsync();

            return actors;
        }
        //Reviews
        public async Task<List<Review>> GetAllUserReviews(string userId)
        {
            var reviews = await dataContext.Reviews
                .Where(u => u.UserId == userId)
                .ToListAsync();

            return reviews;
        }
        


        // Movie Get methods for API
        public async Task<Movie> GetMovieDataFromId(int id)
        {
            var movie = await dataContext.Movies
                .Where(u => u.MovieId == id)
                .FirstOrDefaultAsync();

            return movie;
        }

        public async Task<List<Movie>> GetMovieDataFromName(string movieName)
        {
            var movieList = await dataContext.Movies
                .Where(u => u.Title.Contains(movieName))
                .ToListAsync();

            return movieList;
        }
    }
}
