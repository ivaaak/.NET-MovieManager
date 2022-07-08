using System.ComponentModel.DataAnnotations;

namespace MovieManager.Api.ApiServices.Models
{
    public class Platform
    {
        public int PlatformId { get; init; }
        
        public string PlatformName { get; set; }

        public decimal Price { get; set; }
    }
}
