namespace MovieManager.Models
{
    public class ActorViewModel
    {
        public int ActorId { get; init; }

        public string FullName { get; set; }

        public string? CountryCode { get; set; }

        public int LanguageId { get; set; }

        //List<Movie> Movies { get; set; }
    }
}