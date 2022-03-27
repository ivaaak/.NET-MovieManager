using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
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

        //public bool? IsPublic { get; set; } = false; 
        //default is private, only make public if user wants to 

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;




        public List<Movie> Movies { get; init; }

        public List<PlaylistMovie> PlaylistMovies { get; set; }


        public User User { get; set; } 
        //need for the one to many?
    }
}
