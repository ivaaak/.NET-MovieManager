using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
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

        //Button click functionality in the MAIN lists view
        [HttpPost]//redirects to main
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
        [HttpPost]
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

        //in movieList
        [HttpPost] //redirects to movieList
        public IActionResult RemoveMovieButtonClickList(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            deleteFromDbService.DeleteMovieFromUserPlaylist(movieId, playlistName, UserName);

            TempData["Error"] = $"Removed movie from {playlistName}!";

            return RedirectToAction("MovieList", "Movie"); //doesnt work, needs parameter of playlist name/id?
        }
        [HttpPost]//redirects to movieList
        public IActionResult FavoriteMovieButtonClickList(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToFavorites(movieId, UserName);

            TempData["Success"] = $"Successfully added movie to favorites!";

            //return RedirectToAction("MovieList", "Movie", new {playlistName = playlistName});
            return RedirectToAction("MovieList", "Movie");
        }

        //in the search results view
        [HttpPost]//redirects to main
        public IActionResult AddMovieToPlaylistButtonClickSearch(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddMovieToUserPlaylist(movieId, playlistName, UserName);

            TempData["Success"] = $"Successfully added movie to {playlistName}!";

            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult AddShowToPlaylistButtonClickSearch(int movieId, string playlistName) 
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddShowToUserPlaylist(movieId, playlistName, UserName);

            TempData["Success"] = $"Successfully added show to {playlistName}!";

            return RedirectToAction("Main", "Movie");
        }


        //Actor
        [HttpPost]
        public IActionResult FavoriteActorButtonClick(int actorId)
        {
            string UserName = this.User.Identity.Name;

            addToDbService.AddActorToUserList(actorId, UserName);

            TempData["Success"] = $"Successfully added actor to favorite actors!";

            return RedirectToAction("Actors", "Home");
        }
        [HttpPost]
        public IActionResult RemoveActorButtonClick(int actorId)
        {
            string UserName = this.User.Identity.Name;

            deleteFromDbService.DeleteActorFromUserList(actorId, UserName);

            TempData["Error"] = $"Removed actor from favorites!";

            return RedirectToAction("Actors", "Home");
        }


        //QrCode
        [HttpPost]
        public IActionResult GenerateQRCodeButtonClick(string playlistId)
        {
            addToDbService.GenerateQRCode(playlistId);
            return RedirectToAction("Playlists", "Home");
        }

        
        //Trailer
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


        //Review
        [HttpPost]
        public IActionResult AddReviewToUsersReviews(ReviewViewModel rvm, string userId, string movieId)
        {
            addToDbService.AddReviewToUsersReviews(rvm, userId, movieId);
            return RedirectToAction("Reviews", "Home");
        }


        //QrCode
        [HttpPost]
        public IActionResult CreateCustomPlaylist(string playlistTitle, string userId)
        {
            addToDbService.CreateCustomPlaylist(playlistTitle, userId);
            return RedirectToAction("Playlists", "Home");
        }
    }
}