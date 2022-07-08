using System.ComponentModel.DataAnnotations;

namespace MovieManager.Api.ApiServices.Models
{
    public class Movie
	{
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public string Overview { get; set; }

        public decimal Rating { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public string? MediaType { get; set; } //TV/Movie/Episode
        public decimal? Popularity { get; set; }
        public string? TrailerLink { get; set; }
        public string? Language { get; set; }
        public int? PlatformId { get; set; }
        public string? Platform { get; set; }

        public List<Playlist> Playlists { get; set; }
        public List<PlaylistMovie> PlaylistMovies { get; set; }
    }
}
