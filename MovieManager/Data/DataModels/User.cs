using Microsoft.AspNetCore.Identity;

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

            Actors = new List<Actor>();
            //saved actors list init
        }


        public List<Playlist> Playlists { get; init; }
        
        public List<Actor> Actors { get; init; }

        //not used atm
        public string? CountryCode { get; set; }
        public string? Language { get; set; }
    }
}
