using Microsoft.Extensions.DependencyInjection;
using MovieManager.Services;
using MovieManager.Services.Repositories;
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
                .BuildServiceProvider();

            //await SeedDbAsync(dbContext);
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
            Assert.Throws<NullReferenceException>(() => service.DeleteMovieFromUserPlaylist(
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
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}