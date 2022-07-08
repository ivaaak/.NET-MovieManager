namespace MovieManager.Api.ApiServices.Models
{
    public class Playlist
    {
        public string PlaylistId { get; set; } = Guid.NewGuid().ToString();
        
        public string PlaylistName { get; set; }

        public bool? IsPublic { get; set; } = false; 

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        public List<Movie> Movies { get; init; }

        public List<PlaylistMovie> PlaylistMovies { get; set; }

        public User User { get; set; } 

        public QRCodeObject QrCode { get; set; }
    }
}
