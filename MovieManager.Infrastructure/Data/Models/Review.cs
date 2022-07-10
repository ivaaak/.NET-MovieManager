using MovieManager.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Infrastructure.Data.Models
{
    public class Review
    {
        [Key]
        public string ReviewId { get; init; }
        
        [Required]
        [StringLength(50)]
        public string UserId { get; init; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        [Required]
        [MinLength(DataConstants.Review.TitleMinLength)]
        [MaxLength(DataConstants.Review.TitleMaxLength)]
        public string ReviewTitle { get; set; }
        
        [Required]
        [MinLength(DataConstants.Review.ContentMinLength)]
        [MaxLength(DataConstants.Review.ContentMaxLength)]
        public string ReviewContent { get; set; }
        
        [Required]
        [Range(0,10)]
        public decimal Rating { get; set; }
    }
}
