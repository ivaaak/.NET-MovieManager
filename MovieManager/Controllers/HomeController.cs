using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieManager.Models;
using MovieManager.Services;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Register() => View("Areas/Identity/Pages/Account/Register");
        public IActionResult Home() => View();

        private readonly IMemoryCache cache;


        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }


        public IActionResult Index()
        {
            Console.WriteLine("Hit controller: Home , hit view: Index");

            var popularMovies = ApiGetPopular.GetPopularMovies(15);
            var popularShows = ApiGetPopular.GetPopularShows(15);

            var model = new IndexViewModel()
            {
                DiscoverMovies = popularMovies,
                DiscoverShows = popularShows,
            };

            return View(model);
        }

        public IActionResult Error() => View();

        //this works
    }
}