using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data;
using MovieManager.Services;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieManager.Test
{
    public class SaveMovieToDbObjectServiceTest
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
        /*
        Movie SearchMovieApiToObject(SearchMovie result);
        Movie SearchShowApiToObject(SearchTv result);
        Movie MovieApiToObject(TMDbLib.Objects.Movies.Movie result);
        Movie ShowApiToObject(TvShow result);
        Actor ActorApiToObject(TMDbLib.Objects.People.Person result);
        */

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

            await dbContext.Users.AddAsync(user);
            await dbContext.Movies.AddAsync(movie);
            await dbContext.Playlists.AddAsync(playlist);
            await dbContext.Actors.AddAsync(actor);
            playlist.Movies.Add(movie);
            playlist.PlaylistMovies.Add(playlistMovie);
            user.Playlists.Add(playlist);
            user.Actors.Add(actor);
            await dbContext.SaveChangesAsync();
        }
    }
}