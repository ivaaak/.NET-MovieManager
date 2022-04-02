using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMDbLib.Objects.People;

namespace MovieManager.Data.DataModels
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //this uses the tmdb api id as an id so Auto-ID is turned off
        public int ActorId { get; init; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public string Overview { get; set; }

        public string PhotoUrl { get; set; }

        public MovieCredits? MovieCredits { get; set; }

        public string? CountryCode { get; set; }

        public string? Language { get; set; }
    }
}
