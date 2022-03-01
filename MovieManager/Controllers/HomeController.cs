using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieManager.Data.DBConfig;
using MovieManager.Models;
using MovieManager.Models.DataModels;
using MovieManager.Services.UserServices;
using System.Diagnostics;

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