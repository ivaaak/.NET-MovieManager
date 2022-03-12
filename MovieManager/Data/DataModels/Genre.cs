using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
{
    public class Genre
    {
        [Key]
        public int GenreId { get; init; }
        
        [Required]
        [StringLength(50)]
        public string GenreName { get; set; }
        
    }
}
