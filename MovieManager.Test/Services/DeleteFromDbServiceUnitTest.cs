using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data;
using MovieManager.Services;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MovieManager.Test
{
    public class DeleteFromDbServiceUnitTest
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
                .AddSingleton<IDeleteFromDbService, DeleteFromDbService>()
                .AddSingleton<ISaveMovieToDbObjectService, SaveMovieToDbObjectService>()
                .BuildServiceProvider();

            var testDbContext = serviceProvider.GetService<MovieContext>();
            await SeedDbAsync(testDbContext);
        }


        [Test]
        public void DeleteFromDbUsingId_ValidCall()
        {
            var service = serviceProvider.GetService<IDeleteFromDbService>();
            Assert.DoesNotThrow(() => service.DeleteFromDbUsingId(TestConstants.movie.MovieId)); 
        }
        [Test]
        public void DeleteFromDbUsingName_ValidCall()
        {
            var service = serviceProvider.GetService<IDeleteFromDbService>();
            Assert.DoesNotThrow(() => service.DeleteFromDbUsingName(TestConstants.movie.Title));
        }
        [Test]
        public void DeleteMovieFromUserPlaylist_ValidCall()
        {
            var service = serviceProvider.GetService<IDeleteFromDbService>();
            Assert.DoesNotThrow(() => service.DeleteMovieFromUserPlaylist(
                TestConstants.movie.MovieId, 
                TestConstants.playlist.PlaylistName, 
                TestConstants.user.UserName));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private async Task SeedDbAsync(MovieContext dbContext) 
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