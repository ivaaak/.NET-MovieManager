using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Data.DataModels
{
    public class Movie
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //this is the ID generated from TMDB, AutoId off
        public int MovieId { get; set; }

        [Required]
        [MaxLength(DataConstants.Movie.TitleMaxLength)]
        [MinLength(DataConstants.Movie.TitleMinLength)]
        public string Title { get; set; }

		[Required]
        public string PosterUrl { get; set; }

        [Required]
        [MaxLength(DataConstants.Movie.OverviewMaxLength)]
        [MinLength(DataConstants.Movie.OverviewMinLength)]
        public string Overview { get; set; }

        [Range(0,10)]
        public decimal Rating { get; set; }


        //these are all nullable
        [Range(DataConstants.Movie.YearMinValue, DataConstants.Movie.YearMaxValue)]
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
