using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MovieManagerMVC.Models.DataModels;

namespace MovieManagerMVC.Data.DataModels
{

    using static DataConstants.User;

    public class UserNotAsp : IdentityUser
    {

        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public List<Playlist> Playlists { get; init; }

        public UserNotAsp()
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

        //public string? CountryCode { get; set; }

        //public int? LanguageId { get; set; }
    }
}
