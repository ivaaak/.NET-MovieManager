namespace MovieManager.Models
{
    public class ActorViewModel
    {
        public int ActorId { get; init; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string? CountryCode { get; set; }

        public string Images { get; set; }

        public string MovieCredits { get; set; }

        //List<Movie> Movies { get; set; }
    }
}