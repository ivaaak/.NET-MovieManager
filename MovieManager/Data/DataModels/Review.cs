using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagerMVC.Models.DataModels
{
    public class Review
    {
        [Key]
        public int ReviewId { get; init; }
        
        [Required]
        [StringLength(50)]
        public string UserId { get; init; }

        [Required]
        public int MovieId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ReviewTitle { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ReviewContent { get; set; }

        public decimal Rating { get; set; }
    }
}
