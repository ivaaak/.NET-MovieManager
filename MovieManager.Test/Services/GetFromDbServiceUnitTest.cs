using Microsoft.Extensions.DependencyInjection;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieManager.Test
{
    public class GetFromDbServiceUnitTest
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
                .AddSingleton<IAddToDbService, AddToDbService>()
                .AddSingleton<ISaveMovieToDbObjectService, SaveMovieToDbObjectService>()
                .BuildServiceProvider();

            //await SeedDbAsync(dbContext);
            //SQLite Error 1: 'no such table: User'.
            /*
            Microsoft.EntityFrameworkCore.DbUpdateException : 
            An error occurred while saving the entity changes.See the inner exception for details.
            Microsoft.Data.Sqlite.SqliteException : SQLite Error 1: 'no such table: Users'.
            */
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private async Task SeedDbAsync(InMemoryDbContext dbContext)
        {
            var user = TestConstants.user;
            var movie = TestConstants.movie;
            var playlist = TestConstants.playlist;
            var playlistMovie = TestConstants.playlistMovie;
            var actor = TestConstants.actor;

            playlist.Movies.Add(movie);
            playlist.PlaylistMovies.Add(playlistMovie);
            user.Playlists.Add(playlist);


            await dbContext.AspNetUsers.AddAsync(user);
            await dbContext.Movies.AddAsync(movie);
            await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);
            await dbContext.Actors.AddAsync(actor);
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}