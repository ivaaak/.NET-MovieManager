using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.DataModels;
using MovieManager.Services.ServicesContracts;
using TMDbLib.Objects.Search;

namespace MovieManager.Controllers
{
    public class ButtonController : Controller
    {
        private readonly IAddToDbService addToDbService;
        private readonly IGetFromDbService getFromDbService;


        public ButtonController(
            IGetFromDbService getFromDbService,
            IAddToDbService addToDbService)
        {
            this.getFromDbService = getFromDbService;
            this.addToDbService = addToDbService;
        }

        //Main button click functionality
        [HttpPost]
        public IActionResult AddMovieToPlaylistButtonClick(int movieId, string playlistName) //add playlist name prop?
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToUserPlaylist(movieId, playlistName, UserName);

            return RedirectToAction("Main", "Movie");
        }

        [HttpPost]
        public IActionResult AddShowToPlaylistButtonClick(int movieId, string playlistName) //add playlist name prop?
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddShowToUserPlaylist(movieId, playlistName, UserName);

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