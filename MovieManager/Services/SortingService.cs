using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Services
{
    public class SortingService : ISortingService
    {
        private readonly MovieContext dataContext;

        public SortingService(MovieContext data)
        {
            this.dataContext = data;
        }

        public async Task SortPlaylistByMovieTitle(string userName, string playlistName)
        {
            var playlist = await dataContext.Playlists
                .Include(u => u.Movies)
                .Where(u => u.PlaylistName == playlistName && u.User.UserName == userName)
                .FirstOrDefaultAsync();

            playlist.Movies.OrderBy(x => x.Title);

            await dataContext.SaveChangesAsync();
        }

        public async Task SortPlaylistByMovieRating(string userName, string playlistName)
        {
            var playlist = await dataContext.Playlists
                .Include(u => u.Movies)
                .Where(u => u.PlaylistName == playlistName && u.User.UserName == userName)
                .FirstOrDefaultAsync();

            playlist.Movies.OrderByDescending(x => x.Rating);

            await dataContext.SaveChangesAsync();
        }

        public async Task SortPlaylistByMovieDateAscending(string userName, string playlistName)
        {
            var playlist = await dataContext.Playlists
                .Include(u => u.Movies)
                .Where(u => u.PlaylistName == playlistName && u.User.UserName == userName)
                .FirstOrDefaultAsync();

            playlist.Movies.OrderBy(x => x.ReleaseDate);

            await dataContext.SaveChangesAsync();
        }

        public async Task SortPlaylistByMovieDateDescending(string userName, string playlistName)
        {
            var playlist = await dataContext.Playlists
                .Include(u => u.Movies)
                .Where(u => u.PlaylistName == playlistName && u.User.UserName == userName)
                .FirstOrDefaultAsync();

            playlist.Movies.OrderByDescending(x => x.ReleaseDate);

            await dataContext.SaveChangesAsync();
        }

        //Actors
        public async Task SortActorsByName(string UserName)
        {
            var user = await dataContext.Users
                .Include(u => u.Actors)
                .Where(u => u.UserName == UserName)
                .FirstOrDefaultAsync();

            user.Actors.OrderBy(a => a.FullName);

            await dataContext.SaveChangesAsync();
        }

        //Reviews
        public async Task SortUserReviewsByRating(string userId)
        {
            var userReviews = await dataContext.Reviews
                .Where(u => u.UserId == userId)
                .ToListAsync();

            userReviews.AsQueryable().OrderBy(x => x.Rating);

            await dataContext.SaveChangesAsync();
        }

        public async Task SortUserReviewsByTitle(string userId)
        {
            var userReviews = await dataContext.Reviews
                .Where(u => u.UserId == userId)
                .ToListAsync();

            userReviews.AsQueryable().OrderBy(x => x.ReviewTitle);

            await dataContext.SaveChangesAsync();
        }
    }
}
