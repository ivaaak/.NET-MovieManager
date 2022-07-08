using System.ComponentModel.DataAnnotations;

namespace MovieManager.Api.ApiServices.Models
{
    public class ApplicationFile
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FileName { get; set; }

        public byte[] Content { get; set; }
    }
}
