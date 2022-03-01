using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Models.DataModels
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
