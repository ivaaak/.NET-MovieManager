using Microsoft.AspNetCore.Mvc;
using MovieManager.Infrastructure.Constants;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;
using Microsoft.Extensions.Caching.Distributed;

namespace MovieManager.Controllers
{
    public class DiscoverController : Controller
    {
        private readonly IApiGetListsService apiGetPopularService;

        private readonly IGetFromDbService getFromDbService;

        private readonly IDistributedCache cache;

        public DiscoverController(
            IDistributedCache _cache,
            IApiGetListsService apiGetPopularService,
            IGetFromDbService getFromDbService)
        {
            this.cache = _cache;
            this.apiGetPopularService = apiGetPopularService;
            this.getFromDbService = getFromDbService;
        }

        public IActionResult Start() => View();
        
        //Discover Pages
        public IActionResult Movies()
        {
            var popularMovies = apiGetPopularService.GetPopularMovies(7).Result; //load 7 popular movies/shows

            ViewData[MessageConstant.SuccessMessage] = $"These are the 7 most popular movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = popularMovies,
            };

            return View(model);
        }

        public IActionResult Shows()
        {
            var popularShows = apiGetPopularService.GetPopularShows(7).Result;

            ViewData[MessageConstant.SuccessMessage] = $"These are the 7 most popular shows!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverShows = popularShows,
            };
            return View(model);
        }

        public IActionResult Releases()
        {
            var releasesMovies = apiGetPopularService.GetMovieReleases(15).Result;
            //var releasesShows = apiGetPopularService.GetPopularShows(15);

            ViewData[MessageConstant.SuccessMessage] = $"These are the 15 movie releases!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = releasesMovies,
                //DiscoverShows = releasesShows,
            };
            return View(model);
        }

        public IActionResult Trending()
        {
            var TrendingMovies = apiGetPopularService.GetMovieTrending(15).Result;
            //var TrendingShows = apiGetPopularService.GetShowTrending(15);

            ViewData[MessageConstant.SuccessMessage] = $"These are the 15 trending movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = TrendingMovies,
                //DiscoverShows = TrendingShows,
            };
            return View(model);
        }

        public IActionResult TopRated()
        {
            var TrendingMovies = apiGetPopularService.GetMovieTopRated(15).Result;
            //var TrendingShows = apiGetPopularService.GetShowTrending(15);

            ViewData[MessageConstant.SuccessMessage] = $"These are the 15 top rated movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = TrendingMovies,
                //DiscoverShows = TrendingShows,
            };
            return View(model);
        }


        //TODO new view to showcase publis playlists
        public IActionResult PublicPlaylists()
        {
            var userName = this.User.Identity.Name;

            var playlists = getFromDbService.GetAllPublicPlaylists().Result;
            //var userQrCodes = getFromDbService.GetPlaylistsQRCodes(playlists).Result;

            var model = new PlaylistsViewModel()
            {
                Playlists = playlists,
                //QRCodes = userQrCodes,
            };
            return View(model);
        }
    }
}