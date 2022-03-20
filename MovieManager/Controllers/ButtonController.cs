using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.DataModels;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Objects.Search;

namespace MovieManager.Controllers
{
    public class ButtonController : Controller
    {

        private readonly ILogger<ButtonController> _logger;
        private readonly IGetFromDbService getFromDbService;
        private readonly IAddToDbService addToDbService;


        public ButtonController(
            ILogger<ButtonController> logger,
            ISearchMethodsService searchMethods,
            IApiGetListsService apiGetPopularService,
            IGetFromDbService getFromDbService,
            IAddToDbService addToDbService)
        {
            _logger = logger;
            this.getFromDbService = getFromDbService;
            this.addToDbService = addToDbService;
        }

        //Main button click functionality
        [HttpPost]
        public IActionResult AddMovieToWatchedButtonClick(int movieId) //add playlist name prop?
        {
            string UserName = this.User.Identity.Name; //works

            addToDbService.AddMovieToUserPlaylist(movieId, "watched", "ivo@ivo.com");

            Console.WriteLine($" Movie binded to model: {movieId}");

            return RedirectToAction("Main", "Movie");
        }

        public IActionResult AddShowToWatchedButtonClick(SearchTv show, string PlaylistName)
        {

            Console.WriteLine(" ADD MOVIE TO WATCHED");
            return RedirectToAction("Main", "Movie");
        }

        public IActionResult AddToCurrentButtonClick()
        {
            Console.WriteLine("ADD MOVIE TO CURRENT");
            return RedirectToAction("Main", "Movie");
        }


        //TODO Logic and calling services
        public IActionResult ShowTrailerButtonClick()
        {
            Console.WriteLine(" TRAILER POPUP HERE ");
            return RedirectToAction("Main", "Movie");
        }


        public IActionResult RemoveWatchedButtonClick()
        {
            Console.WriteLine("REMOVE WATCHED MOVIE");
            return RedirectToAction("Main", "Movie");
        }



    }
}