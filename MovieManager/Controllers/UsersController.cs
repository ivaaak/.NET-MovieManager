using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagerMVC.Data.DataModels;

namespace MovieManagerMVC.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<UserNotAsp> UserMngr { get; set; }
        private SignInManager<UserNotAsp> SignInMngr { get; set; }

        public UsersController(UserManager<UserNotAsp> userManager, SignInManager<UserNotAsp> signInManager)
        {
            UserMngr = userManager;
            SignInMngr = signInManager; 
        }

        public async Task<IActionResult> Register()
        {
            UserNotAsp user = await UserMngr.FindByNameAsync("TestUser");
            if (User == null)
            {
                user = new UserNotAsp();
                user.UserName = "testUser";
                user.Email = "testUser@test.com";
                user.FullName = "TestUserName";

                IdentityResult result = await UserMngr.CreateAsync(user, "Test123!");

            }
            return View();
        }



    }
}
