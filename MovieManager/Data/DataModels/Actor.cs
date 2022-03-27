using System.ComponentModel.DataAnnotations;
using TMDbLib.Objects.People;

namespace MovieManager.Data.DataModels
{
    public class Actor
    {
        [Key]
        public int ActorId { get; init; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public string Overview { get; set; }

        public string? CountryCode { get; set; }

        public List<MovieRole> KnownFor { get; set; }

        //public int LanguageId { get; set; }
    }
}
