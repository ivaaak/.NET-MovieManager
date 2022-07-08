namespace MovieManager.Api.ApiServices.Models
{
    public class Review
    {
        public string ReviewId { get; init; }
        
        public string UserId { get; init; }

        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string ReviewTitle { get; set; }

        public string ReviewContent { get; set; }
        
        public decimal Rating { get; set; }
    }
}
