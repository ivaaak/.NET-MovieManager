using Microsoft.AspNetCore.Identity;
using MovieManager.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models.DataModels
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
        public string PlaylistName { get; set; }

        public string? QrCode { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        //[Required]
        //public string UserId { get; set; }

        public List<Movie> Movies { get; init; }

        public User User { get; set; } 
        //need for the one to many?
        public UserPlaylist UserPlaylist { get; set; }
    }
}
