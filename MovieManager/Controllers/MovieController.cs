using Microsoft.AspNetCore.Mvc;
using MovieManagerMVC.Models;
using System.Diagnostics;

namespace MovieManagerMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        public IActionResult MovieCard() => View();

        public IActionResult MoviesCurrent() => View();

        public IActionResult MoviesWatched() => View();

        public IActionResult MoviesFuture() => View();



        public IActionResult Search() => View();

        public IActionResult Discover() => View();

        public IActionResult Releases() => View();

        public IActionResult Review() => View();




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}