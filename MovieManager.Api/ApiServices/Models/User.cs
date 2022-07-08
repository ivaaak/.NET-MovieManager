using Microsoft.AspNetCore.Identity;

namespace MovieManager.Api.ApiServices.Models
{
    public class User : IdentityUser
    {
        public List<Playlist> Playlists { get; init; }
        
        public List<Actor> Actors { get; init; }

        public string? CountryCode { get; set; }
        public string? Language { get; set; }
    }
}
