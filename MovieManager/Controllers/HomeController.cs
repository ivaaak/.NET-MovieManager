using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieManager.Data.DataModels;
using MovieManager.Infrastructure.Constants;
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
            var popularMovies = apiGetPopularService.GetPopularMovies(15).Result; //load 15 popular movies/shows
            var popularShows = apiGetPopularService.GetPopularShows(15).Result;

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
            var userName = this.User.Identity.Name;

            var userPlaylists = getFromDbService.GetAllUserPlaylists(userName).Result;
            var userQrCodes = getFromDbService.GetPlaylistsQRCodes(userPlaylists).Result;

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

            var userPlaylists = getFromDbService.GetUserMovieList(userName, "favorites").Result;

            var model = new MovieListViewModel()
            {
                MoviesList = userPlaylists,
            };

            if (TempData["Success"] != null && TempData.ContainsKey("Success"))
            {
                ViewData[MessageConstant.SuccessMessage] = "Successfully added movie to favorites!";
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Actors()
        {
            var userName = this.User.Identity.Name;

            List<Actor> actors = getFromDbService.GetUserActors(userName).Result;

            var model = new ActorListViewModel()
            {
                Actors = actors,
            };
            if (TempData["Success"] != null && TempData.ContainsKey("Success"))
            {
                ViewData[MessageConstant.SuccessMessage] = Convert.ToString(TempData["Success"]);
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Reviews()
        {
            var userName = this.User.Identity.Name;
            var userId = getFromDbService.GetUserIdFromUserName(userName).Result;

            List<Review> reviews = getFromDbService.GetAllUserReviews(userId).Result;

            var model = new ReviewListViewModel()
            {
                Reviews = reviews,
            };

            return View(model);
        }

        public IActionResult Profile()
        {
            var userName = this.User.Identity.Name;

            var userPlaylists = getFromDbService.GetAllUserPlaylists(userName).Result;

            var model = new PlaylistsViewModel()
            {
                Playlists = userPlaylists,
            };

            return View(model);
        }
        public IActionResult Error() => View();

    }
}