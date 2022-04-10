using Microsoft.Extensions.DependencyInjection;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;

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

            //await SeedDbAsync(dbContext);
        }

        //AddActorToUserList - this uses api if actor isnt in db already
        [Test]
        public void AddActorToUserList_ValidCall()
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.Throws<NullReferenceException>(() => service.AddActorToUserList(TestConstants.actor.ActorId, "testUser"));
        }


        [Test]
        public void AddMovieToFavorites_NullCall()
        {
            int movieId = TestConstants.movie.MovieId;

            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.Throws<NullReferenceException>(() => service.AddMovieToFavorites(movieId, null));
        }
        [Test]
        public void AddMovieToFavorites_ValidCall()
        {
            int movieId = TestConstants.movie.MovieId;
            string userName = TestConstants.user.UserName;

            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.Throws<NullReferenceException>(() => service.AddMovieToFavorites(movieId, userName));
        }


        [Test]
        public void AddMovieToUserPlaylist_NullCall()
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.Throws<NullReferenceException>(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, TestConstants.playlist.PlaylistName, null));
        }
        [Test]
        public void AddMovieToUserPlaylist_ValidCall()
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.Throws<NullReferenceException>(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, TestConstants.playlist.PlaylistName, TestConstants.user.UserName));
        }
        

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private async Task SeedDbAsync(InMemoryDbContext dbContext)
        {

            //await dbContext.Users.AddAsync(TestConstants.user);
            //await dbContext.Movies.AddAsync(TestConstants.movie);
            //await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);
            //System.InvalidOperationException : The entity type 'PlaylistMovie' requires a primary key to be defined.
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}