using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieManager.Data.DataModels;
using MovieManager.Infrastructure.Constants;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> logger;

        private readonly IDistributedCache cache;

        private readonly IFileUploadService fileService;


        public AboutController(
            ILogger<AboutController> _logger, 
            IDistributedCache _cache,
            IFileUploadService _fileService)
        {
            this.logger = _logger;
            this.cache = _cache;
            this.fileService = _fileService;
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
            }
            return View(nameof(Index), cachedData);
        }



        [HttpGet]
        public IActionResult FileUpload()
        {
            TempData[MessageConstant.SuccessMessage] = "File Upload Demo View";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        var fileToSave = new ApplicationFile()
                        {
                            FileName = file.FileName,
                            Content = stream.ToArray()
                        };

                        await fileService.SaveFileAsync(fileToSave);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "AboutController/FileUpload");

                TempData[MessageConstant.ErrorMessage] = "File Upload Failed";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully uploaded the file.";

            return RedirectToAction(nameof(Index));
        }

    }
}