using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Infrastructure.Constants;
using MovieManager.Models;
using MovieManager.Services.ServicesContracts;
using System.Diagnostics;

namespace MovieManager.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly ISearchMethodsService searchMethods;
        private readonly IApiGetListsService apiGetPopularService;
        private readonly IGetFromDbService getFromDbService;

        public MovieController(
            ILogger<MovieController> logger,
            ISearchMethodsService searchMethods,
            IApiGetListsService apiGetPopularService,
            IGetFromDbService getFromDbService)
        {
            _logger = logger;
            this.searchMethods = searchMethods;
            this.apiGetPopularService = apiGetPopularService;
            this.getFromDbService = getFromDbService;
        }


        [Authorize]
        [Route("Movie/Main")]
        public IActionResult Main()
        {
            var userName = this.User.Identity.Name;
            var watched = getFromDbService.GetUserMovieList(userName, "watched");
            var current = getFromDbService.GetUserMovieList(userName, "current");
            var future = getFromDbService.GetUserMovieList(userName, "future");

            if (TempData["Success"] != null && TempData.ContainsKey("Success"))
            {
                ViewData[MessageConstant.SuccessMessage] = Convert.ToString(TempData["Success"]);
            } 
            else if(TempData["Error"] != null && TempData.ContainsKey("Error"))
            {
                ViewData[MessageConstant.ErrorMessage] = Convert.ToString(TempData["Error"]);
            }

            MovieListViewModel movieListViewModel = new MovieListViewModel()
            {
                MoviesList = watched,
                MoviesList2 = current,
                MoviesList3 = future
            };

            return View(movieListViewModel);
        }


        //Search
        public IActionResult Search()
        {
            return View();
        }
        //Post Request on Search Bar Submit
        [HttpPost]
        public IActionResult Search(SearchResultViewModel model)
        {
            var movieResults = searchMethods.SearchMovieTitleToList(model.SearchTerm);
            var showResults = searchMethods.SearchShowTitleToList(model.SearchTerm);
            
            if(movieResults.Count != 0 && showResults.Count != 0)
            {
                ViewData[MessageConstant.SuccessMessage] = $"Found {movieResults.Count} movies and {showResults.Count} shows!";
            }
            else if(movieResults.Count == 0)
            {
                ViewData[MessageConstant.ErrorMessage] = $"No movies found for {model.SearchTerm}";
            }
            else // 0 shows
            {
                ViewData[MessageConstant.ErrorMessage] = $"No shows found for {model.SearchTerm}";
            }

            var results = new SearchResultViewModel()
            {
                ResultMovieList = movieResults,
                ResultShowList = showResults,
                SearchTerm = model.SearchTerm
            };
            Console.WriteLine($"Searching for {model.SearchTerm}");

            return View("SearchResult", results);
        }
        //Search Results Page - SearchResultViewModel 
        public IActionResult SearchResult(SearchResultViewModel results)
        {
            return View();
        }


        //Movie - Show - Actor - Review card pages
        [Route("Movie/MovieCard/{id}")]
        public IActionResult MovieCard(int id)
        {
            var movieIdResult = searchMethods.SearchApiWithMovieID(id);

            return View(movieIdResult);
        }
        [Route("Movie/ShowCard/{id}")]
        public IActionResult ShowCard(int id)
        {
            var showIdResult = searchMethods.SearchApiWithShowID(id);

            return View(showIdResult);
        }
        [Route("Movie/ActorCard/{id}")]
        public IActionResult ActorCard(int id)
        {
            var model = searchMethods.GetActorWithID(id);
            if(model == null)
            {
                ViewData[MessageConstant.ErrorMessage] = $"No actor with an Id of {id} found!";
            }
            else
            {
                ViewData[MessageConstant.SuccessMessage] = $"Actor {model.Person.Name} found!";
            }
            return View(model);
        }
        [Route("Movie/Review/{id}")]
        public IActionResult Review(int id)
        {
            var movie = searchMethods.SearchApiWithMovieID(id);
            var reviews = searchMethods.GetReviewWithMovieID(id);

            movie.Reviews = reviews;

            return View(movie);
        }
        [Route("Movie/ShowReview/{id}")]
        public IActionResult ShowReview(int id)
        {
            var movie = searchMethods.SearchApiWithShowID(id);
            var reviews = searchMethods.GetReviewWithShowID(id);

            movie.Reviews = reviews;

            return View(movie);
        }


        //playlists are user-specific
        [Authorize]
        [Route("Movie/MovieList/{playlistName}")]
        [HttpPost]
        public IActionResult MovieList(string playlistName)
        {
            var userName = this.User.Identity.Name;

            //use temp data to pass messages 
            if (TempData["Success"] != null && TempData.ContainsKey("Success"))
            {
                ViewData[MessageConstant.SuccessMessage] = Convert.ToString(TempData["Success"]);
            }
            else if (TempData["Error"] != null && TempData.ContainsKey("Error"))
            {
                ViewData[MessageConstant.ErrorMessage] = Convert.ToString(TempData["Error"]);
            }

            var movies = getFromDbService.GetUserMovieList(userName, playlistName);

            var model = new MovieListViewModel()
            {
                MoviesList = movies,
                MoviesListName = playlistName,
            };

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}