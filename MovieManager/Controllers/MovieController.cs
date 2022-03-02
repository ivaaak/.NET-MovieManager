using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Infrastructure.Extensions;
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
            Console.WriteLine("Hit controller: Movie , hit view: Search");

            return View();
        }

        [HttpPost]
        public IActionResult SearchTitle(string Title)
        {
            var userId = this.User.Id();

            Console.WriteLine("Hit controller: Movie , hit view: Search");

            var movieResults = SearchMethods.SearchMovieTitleToList(Title);
            var showResults = SearchMethods.SearchShowTitleToList(Title);

            return View(movieResults);
        }


        public IActionResult SearchResult()
        {
            Console.WriteLine("Hit controller: Movie , hit view: SearchResult");
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