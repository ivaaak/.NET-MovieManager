using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers
{
    public class ButtonController : Controller
    {
        //TODO Logic and calling services
        public IActionResult ShowTrailerButtonClick()
        {
            Console.WriteLine(" TRAILER POPUP HERE ");
            return RedirectToAction("Main", "Movie");
        }


        public IActionResult RemoveWatchedButtonClick()
        {
            Console.WriteLine("REMOVE WATCHED MOVIE");
            return RedirectToAction("Main", "Movie");
        }

        public IActionResult AddToWatchedButtonClick()
        {
            Console.WriteLine(" ADD MOVIE TO WATCHED");
            return RedirectToAction("Main", "Movie");
        }

        public IActionResult AddToCurrentButtonClick()
        {
            Console.WriteLine("ADD MOVIE TO CURRENT");
            return RedirectToAction("Main", "Movie");
        }

    }
}