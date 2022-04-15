using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
{
    public class Platform
    {
        [Key]
        public int PlatformId { get; init; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string PlatformName { get; set; }

        [Range(0,100)]
        public decimal Price { get; set; }
    }
}
