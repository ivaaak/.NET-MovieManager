using MovieManager.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Infrastructure.Data.Models
{
    public class Playlist
    {
        public Playlist()
        {
            this.Movies = new List<Movie>();
        }

        [Key]
        [StringLength(36)]
        public string PlaylistId { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [MinLength(DataConstants.Playlist.TitleMinLength)]
        [MaxLength(DataConstants.Playlist.TitleMaxLength)]
        public string PlaylistName { get; set; }

        public bool? IsPublic { get; set; } = false; 
        //default is private, only make public if user wants to 

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        
        //lists
        public List<Movie> Movies { get; init; }

        public List<PlaylistMovie> PlaylistMovies { get; set; }

        public User User { get; set; } 

        public QRCodeObject QrCode { get; set; }
    }
}
