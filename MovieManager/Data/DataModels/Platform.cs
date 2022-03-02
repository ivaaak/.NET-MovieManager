using System.ComponentModel.DataAnnotations;

namespace MovieManager.Data.DataModels
{
    public class Platform
    {
        [Key]
        public int PlatformId { get; init; }
        
        [Required]
        [StringLength(50)]
        public string PlatformName { get; set; }

        public decimal Price { get; set; }
    }
}
