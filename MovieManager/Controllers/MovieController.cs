using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
using MovieManager.Services;
using MovieManager.Services.ServicesContracts;
using System.Diagnostics;
using System.Text;

namespace MovieManager.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly ISearchMethodsService searchMethods;
        private readonly IApiGetListsService apiGetPopularService;

        public MovieController(
            ILogger<MovieController> logger, 
            ISearchMethodsService searchMethods, 
            IApiGetListsService apiGetPopularService)
        {
            _logger = logger;
            this.searchMethods = searchMethods;
            this.apiGetPopularService = apiGetPopularService;
        }



        //[Authorize]
        public IActionResult Main()
        {
            Console.WriteLine("Hit controller: Movie , hit view: Main");

            var userName = this.User.Identity.Name;
            var watched = GetFromDbService.GetUserMovieList(userName, "current");
            var current = GetFromDbService.GetUserMovieList(userName, "watched");
            var future = GetFromDbService.GetUserMovieList(userName, "future");

            MovieListViewModel movieListViewModel = new MovieListViewModel()
            {
                MoviesList = watched,
                MoviesList2 = current,
                //MoviesList3 = future
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

            Console.WriteLine($"search term - {results.SearchTerm}");

            return View();
        }



        [Route("Movie/ActorCard/{id}")]
        public IActionResult ActorCard(int id)
        {
            var model = searchMethods.GetActorWithID(id);

            foreach (var item in model.MovieCredits.Cast)
            {
                Console.WriteLine($" Title: {item.Title}");
                Console.WriteLine($" Title: {item.Character}");
            }

            return View(model);
        }



        

        public IActionResult Discover()
        {
            var popularMovies = apiGetPopularService.GetPopularMovies(7); //load 7 popular movies/shows
            var popularShows = apiGetPopularService.GetPopularShows(7);   //because the carousel breaks with more

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

            var model = new MovieDiscoverViewModel()
            {
                DiscoverMovies = popularMovies,
                DiscoverShows = popularShows,
            };

            return View(model);
        }


        //[Authorize]
        public IActionResult MovieList()
        {
            Console.WriteLine("Hit controller: Movie , hit view: MovieList");
            //takes a PlaylistId and returns a PlaylistViewModel (List<Movie>) with all movies in it to be displayed
            //can edit the playlist?

            return View();
        }


        public IActionResult Review() => View();


        //This is default from ASP.NET, not sure if its needed
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}