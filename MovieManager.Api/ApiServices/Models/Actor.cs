namespace MovieManager.Api.ApiServices.Models
{
    public class Actor
    {
        public int ActorId { get; init; }

        public string FullName { get; set; }

        public string Overview { get; set; }

        public string PhotoUrl { get; set; }

        public string? CountryCode { get; set; }

        public string? Language { get; set; }
    }
}
