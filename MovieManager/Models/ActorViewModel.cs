using TMDbLib.Objects.People;

namespace MovieManager.Models
{
    public class ActorViewModel
    {
        public Person Person { get; set; }

        public MovieCredits MovieCredits { get; set; }

        public TvCredits TvCredits { get; set; }

        public string PhotoUrl { get; set; }
    }
}