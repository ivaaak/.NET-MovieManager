using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
using MovieManager.Services;
using System.Diagnostics;

namespace MovieManager.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        //[Authorize]
        public IActionResult Main()
        {
            Console.WriteLine("Hit controller: Movie , hit view: Main");
            //add logic for getting movie for each column for each user

            return View();
        }

        //[Authorize]
        public IActionResult MovieList()
        {
            Console.WriteLine("Hit controller: Movie , hit view: MovieList");
            //takes a PlaylistId and returns a PlaylistViewModel (List<Movie>) with all movies in it to be displayed
            //can edit the playlist?

            return View();
        }


        public IActionResult MovieCard(int id)
        {
            Console.WriteLine("Hit controller: Movie , hit view: MovieCard");
            //takes a movie ID and returns a MovieViewModel
            var movieObj = GetFromDB.GetMovieFromDBbyID(id);

            return View();
        }




        public IActionResult Search()
        {
            Console.WriteLine("Hit controller: Movie , hit view: Search WITHOUT PARAM");

            return View();
        }


        [HttpPost]
        public IActionResult Search(SearchResultViewModel model)
        {
            Console.WriteLine("Hit controller: Movie , hit view: Search WITH TITLE PARAM");

            var movieResults = SearchMethods.SearchMovieTitleToList(model.SearchTerm);
            var showResults = SearchMethods.SearchShowTitleToList(model.SearchTerm);

            var results = new SearchResultViewModel()
            {
                ResultMovieList = movieResults,
                ResultShowList = showResults,
                SearchTerm = model.SearchTerm
            };

            Console.WriteLine($"Searching for {model.SearchTerm}");

            var viewWithViewModel = SearchResult(results);

            return View("SearchResult", results);
        }


        public IActionResult SearchResult(SearchResultViewModel results)
        {
            Console.WriteLine("Hit controller: Movie , hit view: SearchResult");

            Console.WriteLine($"search term - {results.SearchTerm}");

            return View();
        }

        public IActionResult Discover() => View();

        public IActionResult Releases() => View();

        public IActionResult Review() => View();




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}