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
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IDeleteFromDbService, DeleteFromDbService>()
                .BuildServiceProvider();

            await SeedDbAsync(dbContext);
            //var repo = serviceProvider.GetService<IApplicationDbRepository>();
        }


        [Test]
        public void DeleteFromDbUsingId_ValidCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IDeleteFromDbService>();
            Assert.DoesNotThrow(() => service.DeleteFromDbUsingId(TestConstants.movie.MovieId));
        }
        [Test]
        public void DeleteFromDbUsingName_ValidCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IDeleteFromDbService>();
            Assert.DoesNotThrow(() => service.DeleteFromDbUsingName(TestConstants.movie.Title));
        }
        [Test]
        public void DeleteMovieFromUserPlaylist_ValidCall(SearchMovie searchMovie)
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
        private async Task SeedDbAsync(InMemoryDbContext dbContext)
        {
            await dbContext.Users.AddAsync(TestConstants.user);
            await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}