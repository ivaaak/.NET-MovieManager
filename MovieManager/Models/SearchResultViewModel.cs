using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel() { }

        [Required]
        public string SearchTerm { get; init; }
        //save the search input to display on the top of the results page

        public List<Data.DataModels.Movie> ResultMovieList { get; init; }
        //this is a movie from the DB Model not the API directly
        //from SearchMovieTitleToList

        public List<Data.DataModels.Movie> ResultShowList { get; init; }
        //from SearchShowTitleToList
    }
}
