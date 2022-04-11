using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
{
    public class Review
    {
        [Key]
        public string ReviewId { get; init; }
        
        [Required]
        [StringLength(50)]
        public string UserId { get; init; }

        [Required]
        public string MovieId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ReviewTitle { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ReviewContent { get; set; }
        
        [Required]
        public decimal Rating { get; set; }
    }
}
