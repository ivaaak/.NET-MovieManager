using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.DataModels;

namespace MovieManager.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<User> UserMngr { get; set; }
        private SignInManager<User> SignInMngr { get; set; }

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserMngr = userManager;
            SignInMngr = signInManager; 
        }

        public async Task<IActionResult> Register()
        {
            User user = await UserMngr.FindByNameAsync("TestUser");
            if (User == null)
            {
                user = new User();
                user.UserName = "testUser";
                user.Email = "testUser@test.com";

                IdentityResult result = await UserMngr.CreateAsync(user, "Test123!");

            }
            return View();
        }
    }
}
