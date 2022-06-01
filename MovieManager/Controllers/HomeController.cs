using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieManager.Data.DataModels;
using MovieManager.Infrastructure.Constants;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiGetListsService apiGetPopularService;

        private readonly IGetFromDbService getFromDbService;

        private readonly IDistributedCache cache;

        public HomeController(
            IDistributedCache _cache,
            IApiGetListsService apiGetPopularService,
            IGetFromDbService getFromDbService)
        {
            this.cache = _cache;
            this.apiGetPopularService = apiGetPopularService;
            this.getFromDbService = getFromDbService;
        }

        
        public IActionResult Index()
        {
            var model = new IndexViewModel();
            try
            {
                var popularMovies = apiGetPopularService.GetPopularMovies(15).Result; //load 15 popular movies/shows
                var popularShows = apiGetPopularService.GetPopularShows(15).Result;
                model = new IndexViewModel()
                {
                    DiscoverMovies = popularMovies,
                    DiscoverShows = popularShows,
                };
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return View(model);
        }

        [Authorize]
        public IActionResult Playlists()
        {
            var model = new PlaylistsViewModel();
            var userName = this.User.Identity.Name;
            try
            {
                var userPlaylists = getFromDbService.GetAllUserPlaylists(userName).Result;
                var userQrCodes = getFromDbService.GetPlaylistsQRCodes(userPlaylists).Result;

                model = new PlaylistsViewModel()
                {
                    Playlists = userPlaylists,
                    QRCodes = userQrCodes,
                };
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            if (TempData["Success"] != null && TempData.ContainsKey("Success"))
            {
                ViewData[MessageConstant.SuccessMessage] = Convert.ToString(TempData["Success"]);
            }
            else if (TempData["Error"] != null && TempData.ContainsKey("Error"))
            {
                ViewData[MessageConstant.ErrorMessage] = Convert.ToString(TempData["Error"]);
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Favorites()
        {
            var userName = this.User.Identity.Name;
            var model = new MovieListViewModel();
            try
            {
                var userPlaylists = getFromDbService.GetUserMovieList(userName, "favorites").Result;
                model = new MovieListViewModel()
                {
                    MoviesList = userPlaylists,
                };
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            if (TempData["Success"] != null && TempData.ContainsKey("Success"))
            {
                ViewData[MessageConstant.SuccessMessage] = Convert.ToString(TempData["Success"]);
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Actors()
        {
            var userName = this.User.Identity.Name;
            var model = new ActorListViewModel();
            try
            {
                List<Actor> actors = getFromDbService.GetUserActors(userName).Result;
                model = new ActorListViewModel()
                {
                    Actors = actors,
                };
            }
            catch (Exception ex) { Console.WriteLine(ex); }

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
            var model = new ReviewListViewModel();
            try
            {
                var userId = getFromDbService.GetUserIdFromUserName(userName).Result;
                List<Review> reviews = getFromDbService.GetAllUserReviews(userId).Result;
                model = new ReviewListViewModel()
                {
                    Reviews = reviews,
                };
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return View(model);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userName = this.User.Identity.Name;
            var model = new PlaylistsViewModel();
            try
            {
                var userPlaylists = getFromDbService.GetAllUserPlaylists(userName).Result;
                model = new PlaylistsViewModel()
                {
                    Playlists = userPlaylists,
                };
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return View(model);
        }

        //All public playlists 
        public IActionResult PublicPlaylists()
        {
            var model = new PlaylistsViewModel();
            try
            {
                var allPlaylists = getFromDbService.GetAllPublicPlaylists().Result;
                model.Playlists.AddRange(allPlaylists);
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return View(model);
        }

        public IActionResult Error() => View();
    }
}