namespace MovieManager.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel() { }

        //not used currently because of ASP.NET IDENTITY

        public string Username { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string ConfirmPassword { get; init; }
    }
}
