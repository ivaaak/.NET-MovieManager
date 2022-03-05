using MovieManager.Data.DataModels;
using TMDbLib.Objects.People;

namespace MovieManager.Models
{
    public class MovieCardViewModel
    {
        public Movie Movie { get; set; }

        public MovieCredits MovieCredits { get; set; }
    }
}
