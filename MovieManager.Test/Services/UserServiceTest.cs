using Microsoft.Extensions.DependencyInjection;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
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
                .AddSingleton<IAddToDbService, AddToDbService>()
                .AddSingleton<ISaveMovieToDbObjectService, SaveMovieToDbObjectService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();

            //await SeedDbAsync(dbContext);
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
            await dbContext.Users.AddAsync(TestConstants.user);
            await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);//fix this
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}