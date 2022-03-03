namespace MovieManager.Models
{
    public class SearchResultViewModel
    {
        public string SearchTerm { get; set; }
        //save the search input to display on the top of the results page

        public List<Data.DataModels.Movie> ResultMovieList { get; set; }
        //this is a movie from the DB Model not the API directly
        //from SearchMovieTitleToList

        public List<Data.DataModels.Movie> ResultShowList { get; set; }
        //from SearchShowTitleToList
    }
}
