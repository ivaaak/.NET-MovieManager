using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static MovieManager.Areas.Admin.AdminConstants;

namespace MovieManager.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
