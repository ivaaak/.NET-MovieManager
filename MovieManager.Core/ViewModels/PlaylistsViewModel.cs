using MovieManager.Infrastructure.Constants;
using MovieManager.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Core.ViewModels
{
    public class PlaylistsViewModel
    {
        public PlaylistsViewModel() { }

        public List<Playlist> Playlists { get; set; }

        public Dictionary<string, QRCodeObject> QRCodes { get; set; }

        [MaxLength(DataConstants.Playlist.TitleMaxLength)]
        [MinLength(DataConstants.Playlist.TitleMinLength)]
        public string Title { get; set; }

        public string IsPublic { get; set; }
    }
}