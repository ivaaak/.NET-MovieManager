namespace MovieManager.Models
{
    public class ReviewViewModel
    {
        public string ReviewTitle { get; set; }

        public string ReviewContent { get; set; }

        public decimal Rating { get; set; }

        public string UserId { get; set; }

        public string MovieId { get; set; }
    }
}