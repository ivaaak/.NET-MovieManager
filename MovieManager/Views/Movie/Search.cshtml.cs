using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieManager.Views.Movie
{
    public class Search : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnPost()
        {
            Console.WriteLine($"SEARCH: {SearchTerm}");
        }
    }
}
