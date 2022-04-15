using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServiceContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieManager.Test
{
    public class UserServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            var testDbContext = serviceProvider.GetService<MovieContext>();

            await SeedDbAsync(testDbContext);
        }

        //AddActorToUserList - this uses api if actor isnt in db already
        [Test]
        public void GetUsers_ValidCall()
        {
            var service = serviceProvider.GetService<IUserService>();
            Assert.DoesNotThrow(() => service.GetUsers());
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private async Task SeedDbAsync(MovieContext dbContext) //should be in memory/ repo?
        {
            var user = TestConstants.user;
            var movie = TestConstants.movie;
            var playlist = TestConstants.playlist;
            var playlistMovie = TestConstants.playlistMovie;
            var actor = TestConstants.actor;

            playlist.Movies.Add(movie);
            playlist.PlaylistMovies.Add(playlistMovie);
            user.Playlists.Add(playlist);

            await dbContext.Users.AddAsync(user);
            await dbContext.Movies.AddAsync(movie);
            await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);
            await dbContext.Actors.AddAsync(actor);
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}