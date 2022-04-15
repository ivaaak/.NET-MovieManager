using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel() { }

        [Required]
        public string SearchTerm { get; init; }

        public List<Data.DataModels.Movie> ResultMovieList { get; init; }

        public List<Data.DataModels.Movie> ResultShowList { get; init; }
    }
}
