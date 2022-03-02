using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
{
    public class Actor
    {
        [Key]
        public int ActorId { get; init; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public string? CountryCode { get; set; }

        public int LanguageId { get; set; }

        //public IEnumerable<Movie> Movies { get; init; } = new List<Movie>();
        //many to many
    }
}
