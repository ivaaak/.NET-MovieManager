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
            Console.WriteLine("Hit controller: Movie , hit view: Main");

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
                //MoviesList3 = allMovies,
            };

            return View(movieListViewModel);
        }



        [Route("Movie/MovieCard/{id}")]
        public IActionResult MovieCard(int id) //takes moviecard viewmodel
        {
            Console.WriteLine($"Hit controller: Movie , hit view: MovieCard, ID = {id}");

            var movieIdResult = searchMethods.SearchApiWithMovieID(id);

            return View(movieIdResult);
        }


        [Route("Movie/ShowCard/{id}")]
        public IActionResult ShowCard(int id) //takes moviecard viewmodel
        {
            Console.WriteLine($"Hit controller: Show , hit view: ShowCard, ID = {id}");

            var showIdResult = searchMethods.SearchApiWithShowID(id);

            return View(showIdResult);
        }



        //Initial Search Get Page
        public IActionResult Search()
        {
            Console.WriteLine("Hit controller: Movie , hit view: Search WITHOUT PARAM");

            return View();
        }

        //Search Post Request on Search Bar Submit
        [HttpPost]
        public IActionResult Search(SearchResultViewModel model)
        {
            Console.WriteLine("Hit controller: Movie , hit view: Search WITH TITLE PARAM");

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

        //Search Results Page - gets the View with The SearchResultViewModel (Show and Movie Results Lists)
        public IActionResult SearchResult(SearchResultViewModel results)
        {
            Console.WriteLine("Hit controller: Movie , hit view: SearchResult");

            return View();
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



        public IActionResult Discover()
        {
            var popularMovies = apiGetPopularService.GetPopularMovies(7); //load 7 popular movies/shows
            var popularShows = apiGetPopularService.GetPopularShows(7);   //because the carousel breaks with more


            ViewData[MessageConstant.SuccessMessage] = $"These are the 7 most popular movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = popularMovies,
                DiscoverShows = popularShows,
            };

            return View(model);
        }

        public IActionResult Releases()
        {
            var popularMovies = apiGetPopularService.GetPopularMovies(15); //load 15 popular movies/shows
            var popularShows = apiGetPopularService.GetPopularShows(15);

            ViewData[MessageConstant.SuccessMessage] = $"These are the 15 most popular movies!";

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = popularMovies,
                DiscoverShows = popularShows,
            };

            return View(model);
        }



        [Authorize]
        [Route("Movie/MovieList/{playlistName}")]
        [HttpPost]
        public IActionResult MovieList(string playlistName, string? messageFlag)
        {
            Console.WriteLine("Hit controller: Movie , hit view: MovieList");

            var userName = this.User.Identity.Name;

            if(messageFlag == "added")
            {
                ViewData[MessageConstant.SuccessMessage] = $"Successfully added movie to favorites! ";
            } else if (messageFlag == "removed"){
                ViewData[MessageConstant.ErrorMessage] = $"Removed movie from {playlistName}! ";
            }

            var movies = getFromDbService.GetUserMovieList(userName, playlistName);

            var model = new MovieListViewModel()
            {
                MoviesList = movies,
                MoviesListName = playlistName,
            };

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



        //This is default from ASP.NET, not sure if its needed
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}