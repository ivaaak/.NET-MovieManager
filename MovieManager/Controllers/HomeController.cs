using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieManager.Data.DataModels;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiGetListsService apiGetPopularService;

        private readonly IMemoryCache cache;

        private readonly IGetFromDbService getFromDbService;


        public HomeController(
            IMemoryCache cache,
            IApiGetListsService apiGetPopularService,
            IGetFromDbService getFromDbService)
        {
            this.cache = cache;
            this.apiGetPopularService = apiGetPopularService;
            this.getFromDbService = getFromDbService;
        }


        public IActionResult Index()
        {
            Console.WriteLine("Hit controller: Home , hit view: Index");

            var popularMovies = apiGetPopularService.GetPopularMovies(15); //load 15 popular movies/shows
            var popularShows = apiGetPopularService.GetPopularShows(15);

            var model = new IndexViewModel()
            {
                DiscoverMovies = popularMovies,
                DiscoverShows = popularShows,
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Playlists()
        {
            Console.WriteLine("Hit controller: Home , hit view: Playlists");

            var userName = this.User.Identity.Name;

            var userPlaylists = getFromDbService.GetAllUserPlaylists(userName);
            var userQrCodes = getFromDbService.GetPlaylistsQRCodes(userPlaylists);

            var model = new PlaylistsViewModel()
            {
                Playlists = userPlaylists,
                QRCodes = userQrCodes,
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Favorites()
        {
            var userName = this.User.Identity.Name;

            var userPlaylists = getFromDbService.GetUserMovieList(userName, "favorites");

            var model = new MovieListViewModel()
            {
                MoviesList = userPlaylists,
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Actors()
        {
            var userName = this.User.Identity.Name;

            List<Actor> actors = getFromDbService.GetUserActors(userName);

            var model = new ActorListViewModel()
            {
                Actors = actors,
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Reviews()
        {
            var userName = this.User.Identity.Name;

            List<Review> reviews = null; //getFromDbService.GetReviews(userName);

            var model = new ReviewListViewModel()
            {
                Reviews = reviews,
            };

            return View(model);
        }

        public IActionResult Error() => View();
        public IActionResult Profile()
        {
            var userName = this.User.Identity.Name;

            var userPlaylists = getFromDbService.GetAllUserPlaylists(userName);

            var model = new PlaylistsViewModel()
            {
                Playlists = userPlaylists,
            };

            return View(model);
        }
    }
}