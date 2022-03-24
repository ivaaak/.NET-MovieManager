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
        private readonly IDeleteFromDbService deleteFromDbService;

        public ButtonController(
            IGetFromDbService getFromDbService,
            IAddToDbService addToDbService,
            IDeleteFromDbService deleteFromDbService)
        {
            this.getFromDbService = getFromDbService;
            this.addToDbService = addToDbService;
            this.deleteFromDbService = deleteFromDbService;
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


        public IActionResult RemoveMovieButtonClick(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            deleteFromDbService.DeleteMovieFromUserPlaylist(movieId, playlistName, UserName);

            return RedirectToAction("Main", "Movie");
        }


        [HttpPost]
        public IActionResult FavoriteMovieButtonClick(int movieId, string playlistName) //add playlist name prop?
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToFavorites(movieId, UserName);

            return RedirectToAction("Main", "Movie");
        }

        //TODO Logic and calling services
        //Make this a popup window/embed trailer from a yt link
        public IActionResult ShowTrailerButtonClick()
        {
            Console.WriteLine(" TRAILER POPUP HERE ");
            return RedirectToAction("Main", "Movie");
        }
    }
}