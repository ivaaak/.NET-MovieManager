using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace MovieManager.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> logger;

        private readonly IDistributedCache cache;


        public AboutController(
            ILogger<AboutController> _logger, 
            IDistributedCache _cache)
        {
            this.logger = _logger;
            this.cache = _cache;
        }


        public async Task<IActionResult> About() 
        {
            DateTime dateTime = DateTime.Now;
            var cachedData = await cache.GetStringAsync("cachedTime");

            if (cachedData == null)
            {
                cachedData = dateTime.ToString();
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(20),
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60)
                };

                await cache.SetStringAsync("cachedTime", cachedData, cacheOptions);
                //await cache.SetAsync()
            }

            return View(nameof(Index), cachedData);
        } 

        public async Task<IActionResult> Privacy()
        {
            DateTime dateTime = DateTime.Now;
            var cachedData = await cache.GetStringAsync("cachedTime");

            if (cachedData == null)
            {
                cachedData = dateTime.ToString();
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(20),
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60)
                };

                await cache.SetStringAsync("cachedTime", cachedData, cacheOptions);
                //await cache.SetAsync();
            }

            return View(nameof(Index), cachedData);
        }
    }
}