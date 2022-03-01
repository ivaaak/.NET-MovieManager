using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Models.DataModels
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
