using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
