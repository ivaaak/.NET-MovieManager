using Microsoft.AspNetCore.Mvc;
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
            try
            {
                addToDbService.AddMovieToUserPlaylist(movieId, playlistName, UserName);
                TempData["Success"] = $"Successfully added movie to {playlistName}!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add show!";
            }
            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult AddShowToPlaylistButtonClick(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                addToDbService.AddShowToUserPlaylist(movieId, playlistName, UserName);
                TempData["Success"] = $"Successfully added show to {playlistName}!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add show!";
            }
            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult RemoveMovieButtonClick(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                deleteFromDbService.DeleteMovieFromUserPlaylist(movieId, playlistName, UserName);
                TempData["Error"] = $"Removed movie from {playlistName}!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt remove movie!";
            }
            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult FavoriteMovieButtonClick(int movieId)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                addToDbService.AddMovieToFavorites(movieId, UserName);
                TempData["Success"] = $"Successfully added movie to favorites!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add movie!";
            }
            return RedirectToAction("Favorites", "Home");
        }

        //in movieList
        [HttpPost] 
        public IActionResult RemoveMovieButtonClickList(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                deleteFromDbService.DeleteMovieFromUserPlaylist(movieId, playlistName, UserName);
                TempData["Error"] = $"Removed movie from {playlistName}!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt remove movie!";
            }
            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult FavoriteMovieButtonClickList(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                addToDbService.AddMovieToFavorites(movieId, UserName);
                TempData["Success"] = $"Successfully added movie to favorites!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add movie!";
            }

            //return RedirectToAction("MovieList", "Movie", new {playlistName = playlistName});
            return RedirectToAction("Main", "Movie");
        }

        //in the search results view
        [HttpPost]//redirects to main
        public IActionResult AddMovieToPlaylistButtonClickSearch(int movieId, string playlistName)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                addToDbService.AddMovieToUserPlaylist(movieId, playlistName, UserName);
                TempData["Success"] = $"Successfully added movie to {playlistName}!";
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add show!";
            }
            return RedirectToAction("Main", "Movie");
        }
        [HttpPost]
        public IActionResult AddShowToPlaylistButtonClickSearch(int movieId, string playlistName) 
        {
            string UserName = this.User.Identity.Name;

            try
            {
                addToDbService.AddShowToUserPlaylist(movieId, playlistName, UserName);
                TempData["Success"] = $"Successfully added show to {playlistName}!";
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add show!";
            }
            return RedirectToAction("Main", "Movie");
        }


        //Actor
        [HttpPost]
        public IActionResult FavoriteActorButtonClick(int actorId)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                addToDbService.AddActorToUserList(actorId, UserName);
                TempData["Success"] = $"Successfully added actor to favorite actors!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add actor!";
            }
            return RedirectToAction("Actors", "Home");
        }
        [HttpPost]
        public IActionResult RemoveActorButtonClick(int actorId)
        {
            string UserName = this.User.Identity.Name;
            try
            {
                deleteFromDbService.DeleteActorFromUserList(actorId, UserName);
                TempData["Error"] = $"Removed actor from favorites!";
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt remove actor from favorites!";
            }
            return RedirectToAction("Actors", "Home");
        }


        //QrCode
        [HttpPost]
        public IActionResult GenerateQRCodeButtonClick(string playlistId)
        {
            try
            {
                addToDbService.GenerateQRCode(playlistId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt generate QR Code for the playlist!";
            }
            return RedirectToAction("Playlists", "Home");
        }

        
        //Trailer
        [HttpGet]
        public IActionResult ShowTrailerButtonClick(int Id, string MediaType)
        {
            string trailerLink = String.Empty;
            try
            {
                if (MediaType == "movie")
                {
                    trailerLink = searchMethodsService.GetMovieTrailer(Id).Result;
                }
                else
                {
                    trailerLink = searchMethodsService.GetShowTrailer(Id).Result;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt fetch a trailer from the API.";
            }

            return PartialView("_TrailerPartial");
        }


        //Review
        [HttpPost]
        public IActionResult AddReviewToUsersReviews(string reviewTitle, 
            string reviewContent, decimal rating, 
            int movieId, string movieTitle)
        {
            var userName = User.Identity.Name;
            try
            {
                addToDbService.AddReviewToUsersReviews(reviewTitle, reviewContent, rating, userName, movieId, movieTitle);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt add the review!";
            }
            return RedirectToAction("Reviews", "Home");
        }


        //QrCode
        [HttpPost]
        public IActionResult CreateCustomPlaylist(string playlistTitle)
        {
            Console.WriteLine($"THE PLAYLIST NAME BOUND IS  {playlistTitle}");
            var userName = User.Identity.Name;
            try
            {
                var userId = getFromDbService.GetUserIdFromUserName(userName).Result;
                addToDbService.CreateCustomPlaylist(playlistTitle, userId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                TempData["Error"] = $"Couldnt create playlist!";
            }
            return RedirectToAction("Playlists", "Home");
        }
    }
}