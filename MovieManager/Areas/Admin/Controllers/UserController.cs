using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Data.DataModels;
using MovieManager.Infrastructure.Constants;
using MovieManager.Models.User;
using MovieManager.Services.ServiceContracts;

namespace MovieManager.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<User> userManager;

        private readonly IUserService service;

        public UserController(
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

        public async Task<IActionResult> ManageUsers()
        {
            var users = await service.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await service.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.UserName}"
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await service.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }


        public async Task<IActionResult> Edit(string id)
        {
            var model = await service.GetUserForEdit(id);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await service.UpdateUser(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Successfully updated the user!";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "There was an error updating the user!";
            }

            return View(model);
        }

        //remove authentication and area in base controller
        //before creating a role if not admin account
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
