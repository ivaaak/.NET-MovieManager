using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models.User
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
    }
}
