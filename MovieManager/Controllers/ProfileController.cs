using Microsoft.AspNetCore.Mvc;
using MovieManagerMVC.Models;
using System.Diagnostics;

namespace MovieManagerMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index() => View();

        public IActionResult IndexMovies() => View();

        public IActionResult IndexMoviesNoBS() => View();

        public IActionResult Review() => View();

            


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}