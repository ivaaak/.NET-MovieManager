using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models.DataModels
{
    public class UserNoIden
    {
        public UserNoIden()
        {
            Playlists = new List<Playlist>();
            this.Playlists.Add(new Playlist
            {
                PlaylistName = "watched"
            });
            this.Playlists.Add(new Playlist
            {
                PlaylistName = "current"
            });
            this.Playlists.Add(new Playlist
            {
                PlaylistName = "future"
            });
            //generate the 3 default playlists when creating an user
            //playlist instance might need some more fields
        }

        [Key]
        [StringLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(32)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }


        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        //public string? CountryCode { get; set; }

        //public int? LanguageId { get; set; }


        public List<Playlist> Playlists { get; init; }
    }
}
