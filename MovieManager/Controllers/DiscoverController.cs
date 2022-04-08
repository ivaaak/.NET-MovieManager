using Microsoft.AspNetCore.Mvc;
using MovieManager.Infrastructure.Constants;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Controllers
{
    public class DiscoverController : Controller
    {
        private readonly ISearchMethodsService searchMethods;
        private readonly IApiGetListsService apiGetPopularService;
        private readonly IGetFromDbService getFromDbService;

        public DiscoverController(
            ISearchMethodsService searchMethods,
            IApiGetListsService apiGetPopularService,
            IGetFromDbService getFromDbService)
        {
            this.searchMethods = searchMethods;
            this.apiGetPopularService = apiGetPopularService;
            this.getFromDbService = getFromDbService;
        }

        public IActionResult Start() => View();
        
        //Discover Pages
        public IActionResult Movies()
        {
            var popularMovies = apiGetPopularService.GetPopularMovies(7); //load 7 popular movies/shows

            ViewData[MessageConstant.SuccessMessage] = $"These are the 7 most popular movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = popularMovies,
            };

            return View(model);
        }

        public IActionResult Shows()
        {
            var popularShows = apiGetPopularService.GetPopularShows(7);


            ViewData[MessageConstant.SuccessMessage] = $"These are the 7 most popular shows!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverShows = popularShows,
            };

            return View(model);
        }

        public IActionResult Releases()
        {
            //fix it to get latest
            var popularMovies = apiGetPopularService.GetPopularMovies(15);
            var popularShows = apiGetPopularService.GetPopularShows(15);
            //load 15 popular movies/shows from api

            ViewData[MessageConstant.SuccessMessage] = $"These are the 15 most popular movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = popularMovies,
                DiscoverShows = popularShows,
            };

            return View(model);
        }


        public IActionResult PublicPlaylists()
        {
            var userName = this.User.Identity.Name;

            var playlists = getFromDbService.GetAllPublicPlaylists();
            var userQrCodes = getFromDbService.GetPlaylistsQRCodes(playlists);

            var model = new PlaylistsViewModel()
            {
                Playlists = playlists,
                QRCodes = userQrCodes,
            };

            return View(model);
        }

        public IActionResult Trending() => View();
        public IActionResult Latest() => View();
        public IActionResult HighestRated() => View();
    }
}