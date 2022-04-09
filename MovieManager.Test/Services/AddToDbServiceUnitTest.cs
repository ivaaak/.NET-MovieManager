using Microsoft.Extensions.DependencyInjection;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using TMDbLib.Objects.Search;
using Xunit;

namespace MovieManager.Test
{
    public class AddToDbServiceUnitTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IAddToDbService, AddToDbService>()
                .BuildServiceProvider();

            //var repo = serviceProvider.GetService<IApplicationDbRepository>();
        }


        [Fact]
        public void AddMovieToFavorites_ValidCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IAddToDbService>();

            Assert.DoesNotThrow(() => service.AddMovieToFavorites(TestConstants.movie.MovieId, "testUser"));
        }
        [Fact]
        public void AddMovieToFavorites_NullCall(SearchMovie searchMovie)
        {
            var service = serviceProvider.GetService<IAddToDbService>();

            Assert.DoesNotThrow(() => service.AddMovieToFavorites(TestConstants.movie.MovieId, null));
        }
    }
}