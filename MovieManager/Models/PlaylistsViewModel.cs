using MovieManager.Data.DataModels;

namespace MovieManager.Models
{
    public class PlaylistsViewModel
    {
        public PlaylistsViewModel() { }

        public List<Playlist> Playlists { get; set; }
        public List<QRCodeObject> QRCodes { get; set; }
    }
}