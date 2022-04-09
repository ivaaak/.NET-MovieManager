using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data.DataModels;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;
using Xunit;

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
                .BuildServiceProvider();

            await SeedDbAsync(dbContext);
            //var repo = serviceProvider.GetService<IApplicationDbRepository>();
        }

        //AddMovieToFavorites
        [Test]
        public void AddMovieToFavorites_ValidCall(SearchMovie searchMovie)
        {
            string test = "excepasjkld;as";
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToFavorites(TestConstants.movie.MovieId, TestConstants.user.Id), test);
        }
        [Test]
        public void AddMovieToFavorites_NullCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToFavorites(TestConstants.movie.MovieId, null), "test");
        }

        //AddMovieToUserPlaylist
        [Test]
        public void AddMovieToUserPlaylist_ValidCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, "current", "testUser"), "test");
        }
        [Test]
        public void AddMovieToUserPlaylist_NullCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddMovieToUserPlaylist(TestConstants.movie.MovieId, "current", null));
        }

        //AddActorToUserList - this uses api if actor isnt in db already
        [Test]
        public void AddActorToUserList_ValidCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IAddToDbService>();
            Assert.DoesNotThrow(() => service.AddActorToUserList(TestConstants.actor.ActorId, "testUser"));
        }





        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private async Task SeedDbAsync(InMemoryDbContext dbContext)
        {
            await dbContext.Users.AddAsync(TestConstants.user);
            await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}