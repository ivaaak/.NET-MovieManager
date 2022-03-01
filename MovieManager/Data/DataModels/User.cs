using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MovieManager.Models.DataModels;

namespace MovieManager.Data.DataModels
{
    public class User : IdentityUser
    {
        public User()
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
        }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public List<Playlist> Playlists { get; init; }

        //public string? CountryCode { get; set; }

        //public int? LanguageId { get; set; }
    }
}
