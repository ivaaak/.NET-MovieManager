using System.ComponentModel.DataAnnotations;

namespace MovieManager.Core.ViewModels
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel() { }

        [Required]
        public string SearchTerm { get; init; }

        public List<Infrastructure.Data.Models.Movie> ResultMovieList { get; init; }

        public List<Infrastructure.Data.Models.Movie> ResultShowList { get; init; }
    }
}
