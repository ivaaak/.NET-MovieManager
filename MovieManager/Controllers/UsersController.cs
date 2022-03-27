using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Data.DataModels;
using MovieManager.Services.ServiceContracts;

namespace MovieManager.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<User> userManager;

        private readonly IUserService service;

        public UsersController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<User> _userManager,
            IUserService _service)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }


        //Create roles by changing the 
        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Administrator"
            });
            return Ok();
        }
    }
}
