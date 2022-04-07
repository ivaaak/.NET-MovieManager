using MovieManager.Data.DataModels;

namespace MovieManager.Models
{
    public class PlaylistsViewModel
    {
        public PlaylistsViewModel() { }

        public List<Playlist> Playlists { get; set; }

        public Dictionary<string, QRCodeObject> QRCodes { get; set; }
    }
}