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

        public HomeController(
            IMemoryCache cache,
            IApiGetListsService apiGetPopularService)
        {
            this.cache = cache;
            this.apiGetPopularService = apiGetPopularService;
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

        public IActionResult Error() => View();
    }
}