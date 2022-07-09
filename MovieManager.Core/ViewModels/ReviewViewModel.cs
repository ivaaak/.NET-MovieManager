namespace MovieManager.Core.ViewModels
{
    public class ReviewViewModel
    {
        public string ReviewId { get; set; }

        public string ReviewTitle { get; set; }

        public string ReviewContent { get; set; }

        public decimal Rating { get; set; }

        public int MovieId { get; set; }

        public string MovieTitle { get; set; }
    }
}