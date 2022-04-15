using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMDbLib.Objects.People;

namespace MovieManager.Data.DataModels
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //This uses the Tmdb Api Actor id as its id so Auto-ID is turned off
        public int ActorId { get; init; }

        [Required]
        [MaxLength(DataConstants.Actor.NameMaxLength)]
        [MinLength(DataConstants.Actor.NameMinLength)]
        public string FullName { get; set; }

        [MaxLength(DataConstants.Actor.OverviewMaxLength)]
        [MinLength(DataConstants.Actor.OverviewMinLength)]
        public string Overview { get; set; }

        public string PhotoUrl { get; set; }

        public MovieCredits? MovieCredits { get; set; }

        public string? CountryCode { get; set; }

        public string? Language { get; set; }
    }
}
