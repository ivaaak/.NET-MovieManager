using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MovieManager.Test
{
    public class AddToDbServiceUnitTest
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
                .AddSingleton<ISaveMovieToDbObjectService, SaveMovieToDbObjectService> ()
                .BuildServiceProvider();

            var testDbContext = serviceProvider.GetService<MovieContext>();
            await SeedDbAsync(testDbContext);
            //SQLite Error 1: 'no such table: User'.
            /*
            Microsoft.EntityFrameworkCore.DbUpdateException : 
            An error occurred while saving the entity changes.See the inner exception for details.
            Microsoft.Data.Sqlite.SqliteException : SQLite Error 1: 'no such table: Users'.
            */
        }

        //AddActorToUserList - this uses api if actor isnt in db already
        [Test]
        public void AddActorToUserList_ValidCall()
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddActorToUserList(TestConstants.actor.ActorId, TestConstants.user.UserName));
        }


        [Test]
        public void AddMovieToFavorites_NullCall()
        {
            int movieId = TestConstants.movie.MovieId;

            var service = serviceProvider.GetService<IAddToDbService>();
           Assert.DoesNotThrow(() => service.AddMovieToFavorites(movieId, null));
        }
        [Test]
        public void AddMovieToFavorites_ValidCall()
        {
            int movieId = TestConstants.movie.MovieId;
            string userName = TestConstants.user.UserName;

            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToFavorites(movieId, userName));
        }


        [Test]
        public void AddMovieToUserPlaylist_NullCall()
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, TestConstants.playlist.PlaylistName, null));
            //Assert.Throws<NullReferenceException>(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, TestConstants.playlist.PlaylistName, null));
        }
        [Test]
        public void AddMovieToUserPlaylist_ValidCall()
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, TestConstants.playlist.PlaylistName, TestConstants.user.UserName));
            //Assert.Throws<NullReferenceException>(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, TestConstants.playlist.PlaylistName, TestConstants.user.UserName));
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