using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Models;

namespace MovieManager.Areas.Admin.Controllers
{
    public class ManageController : BaseController
    {
        private readonly MovieContext dataContext;

        public ManageController(MovieContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Movies()
        {
            var movies = dataContext.Movies.Select(x => x).ToList();

            MovieListViewModel model = new MovieListViewModel()
            {
                MoviesList = movies,
            };
            return View(model);
        }
        public IActionResult Playlists()
        {
            var playlists = dataContext.Playlists.Include(a => a.User).Select(x => x).ToList();

            PlaylistsViewModel model = new PlaylistsViewModel()
            {
                Playlists = playlists,
            };
            return View(model);
        }
    }
}
