using Microsoft.AspNetCore.Mvc;
using MovieManager.Infrastructure.Alerts;
using MovieManager.Infrastructure.Constants;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Controllers
{
    public class ButtonController : Controller
    {
        private readonly IAddToDbService addToDbService;
        private readonly IGetFromDbService getFromDbService;
        private readonly IDeleteFromDbService deleteFromDbService;
        private readonly ISearchMethodsService searchMethodsService;


        public ButtonController(
            IGetFromDbService getFromDbService,
            IAddToDbService addToDbService,
            IDeleteFromDbService deleteFromDbService, 
            ISearchMethodsService searchMethodsService)
        {
            this.getFromDbService = getFromDbService;
            this.addToDbService = addToDbService;
            this.deleteFromDbService = deleteFromDbService;
            this.searchMethodsService = searchMethodsService;
        }

        //Main button click functionality
        [HttpPost]
        public IActionResult AddMovieToPlaylistButtonClick(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToUserPlaylist(movieId, playlistName, UserName);

            TempData["Success"] = $"Successfully added movie to {playlistName}!";

            return RedirectToAction("Main", "Movie");
        }

        [HttpPost]
        public IActionResult AddShowToPlaylistButtonClick(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddShowToUserPlaylist(movieId, playlistName, UserName);

            TempData["Success"] = $"Successfully added show to {playlistName}!";

            return RedirectToAction("Main", "Movie");
        }

        //main
        public IActionResult RemoveMovieButtonClick(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            deleteFromDbService.DeleteMovieFromUserPlaylist(movieId, playlistName, UserName);

            TempData["Error"] = $"Removed movie from {playlistName}!";

            return RedirectToAction("Main", "Movie");
        }


        [HttpPost]
        public IActionResult FavoriteMovieButtonClick(int movieId)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToFavorites(movieId, UserName);

            TempData["Success"] = $"Successfully added movie to favorites!";

            return RedirectToAction("Main", "Movie");
        }


        //playlist view
        public IActionResult RemoveMovieButtonClickList(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            deleteFromDbService.DeleteMovieFromUserPlaylist(movieId, playlistName, UserName);

            TempData["Error"] = $"Removed movie from {playlistName}!";

            return RedirectToAction("MovieList", "Movie"); //doesnt work? needs parameter of playlist name/id?
        }
        [HttpPost]
        public IActionResult FavoriteMovieButtonClickList(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToFavorites(movieId, UserName);

            TempData["Success"] = $"Successfully added movie to favorites!";

            //return RedirectToAction("MovieList", "Movie", new {playlistName = playlistName});
            return RedirectToAction("MovieList", "Movie");
        }


        [HttpPost]
        public IActionResult FavoriteActorButtonClick(int actorId)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddActorToUserList(actorId, UserName);

            TempData["Success"] = $"Successfully added actor to favorite actors!";

            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult RemoveActorButtonClick(int actorId)
        {
            string UserName = this.User.Identity.Name;

            deleteFromDbService.DeleteActorFromUserList(actorId, UserName);

            TempData["Error"] = $"Removed actor from favorites!";

            return RedirectToAction("Main", "Movie");
        }




        //TODO Logic and calling services
        //Make this a popup window/embed trailer from a yt link
        [HttpGet]
        public IActionResult ShowTrailerButtonClick(int Id, string MediaType)
        {
            string trailerLink = String.Empty;
            if (MediaType == "movie")
            {
                trailerLink = searchMethodsService.GetMovieTrailer(Id);
            }
            else
            {
                trailerLink = searchMethodsService.GetShowTrailer(Id);
            }
            return PartialView("_TrailerPartial");
        }
    }
}