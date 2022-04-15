using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
{
    public class Genre
    {
        [Key]
        public int GenreId { get; init; }
        
        [Required]
        [MaxLength(DataConstants.Genre.NameMaxLength)]
        [MinLength(DataConstants.Genre.NameMinLength)]
        public string GenreName { get; set; }
    }
}
