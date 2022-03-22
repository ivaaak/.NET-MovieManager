using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieManager.Models;
using MovieManager.Services;
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


        public IActionResult Playlists()
        {
            Console.WriteLine("Hit controller: Home , hit view: Playlists");

            var userName = this.User.Identity.Name;

            var userPlaylists = getFromDbService.GetAllUserPlaylists(userName);

            var model = new PlaylistsViewModel()
            {
                Playlists = userPlaylists,
            };

            return View(model);
        }


        public IActionResult Error() => View();
        public IActionResult Test() => View();
    }
}