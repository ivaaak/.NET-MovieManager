namespace MovieManager.Models
{
    public class TrailerViewModel
    {
        public string? TrailerKey { get; set; } 

        public int? TrailerId { get; set; } 

        public Dictionary<int, string> Trailers { get; set; }
    }
}