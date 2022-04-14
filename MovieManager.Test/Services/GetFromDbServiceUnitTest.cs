using Microsoft.Extensions.DependencyInjection;
using MovieManager.Data;
using MovieManager.Data.DataModels;
using MovieManager.Services;
using MovieManager.Services.Repositories;
using MovieManager.Services.ServicesContracts;
using MovieManager.Test.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManager.Test
{
    public class GetFromDbServiceUnitTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private List<Playlist> playlistList = new List<Playlist>();

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IGetFromDbService, GetFromDbService>()
                .BuildServiceProvider();

            var testDbContext = serviceProvider.GetService<MovieContext>();
            await SeedDbAsync(testDbContext);
        }

        [Test]
        public void GetAllUserPlaylists_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetAllUserPlaylists(TestConstants.user.UserName));
        }

        [Test]
        public void GetAllPublicPlaylists_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetAllPublicPlaylists());
        }

        [Test]
        public void GetUserMovieList_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetUserMovieList(TestConstants.user.UserName, TestConstants.playlist.PlaylistName));
        }

        [Test]
        public void GetUserActors_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetUserActors(TestConstants.user.UserName));
        }

        [Test]
        public void GetAllUserReviews_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetAllUserReviews(TestConstants.user.UserName));
        }

        [Test]
        public void GetUserIdFromUserName_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetUserIdFromUserName(TestConstants.user.UserName));
        }

        [Test]
        public void GetPlaylistsQRCodes_ValidCall()
        {
            var service = serviceProvider.GetService<IGetFromDbService>();
            Assert.DoesNotThrow(() => service.GetPlaylistsQRCodes(playlistList));
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
            var userPlaylist = TestConstants.userPlaylist;

            playlist.Movies.Add(movie);
            playlist.PlaylistMovies.Add(playlistMovie);
            user.Playlists.Add(userPlaylist);
            user.Playlists.Add(playlist);
            user.Actors.Add(actor);

            await dbContext.Users.AddAsync(user);
            await dbContext.Movies.AddAsync(movie);
            await dbContext.Playlists.AddAsync(TestConstants.userPlaylist);
            await dbContext.Playlists.AddAsync(TestConstants.playlist);
            await dbContext.Actors.AddAsync(actor);
            await dbContext.SaveChangesAsync(); //inherited from DbContext
        }
    }
}