using Microsoft.AspNetCore.Mvc;
using MovieManagerMVC.Models;
using System.Diagnostics;

namespace MovieManagerMVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }


        public IActionResult About() => View();

        public IActionResult Privacy() => View();




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}