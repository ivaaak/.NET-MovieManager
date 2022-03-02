using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Register() => View("Areas/Identity/Pages/Account/Register");
        public IActionResult Home() => View();

        private readonly IMemoryCache cache;


        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }


        public IActionResult Index()
        {
            Console.WriteLine("Hit controller: Home , hit view: Index");
            return View();
        }

        public IActionResult Error() => View();

        //this works
    }
}