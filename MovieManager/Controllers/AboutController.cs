using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
using System.Diagnostics;

namespace MovieManager.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }


        public IActionResult About() 
        {
            Console.WriteLine("Controller hit: About, view hit: About");
            return View();
        } 

        public IActionResult Privacy()
        {
            Console.WriteLine("Controller hit: About, view hit: Privacy");
            return View();
        }
    }
}