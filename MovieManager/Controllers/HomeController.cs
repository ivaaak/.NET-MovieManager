using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieManagerMVC.Data.DBConfig;
using MovieManagerMVC.Models;
using MovieManagerMVC.Models.DataModels;
using MovieManagerMVC.Services.UserServices;
using System.Diagnostics;

namespace MovieManagerMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Register() => View();
        public IActionResult Home() => View();

        private readonly IMemoryCache cache;

        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestMovies = new List<Movie>(); //this.cache.Get<List<LatestCarServiceModel>>(LatestCarsCacheKey);

            if (latestMovies == null)
            {
                latestMovies = null; //this.movies.Latest().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                //this.cache.Set(LatestCarsCacheKey, latestCars, cacheOptions);
            }

            return View(latestMovies);
        }

        public IActionResult Error() => View();
    }
}