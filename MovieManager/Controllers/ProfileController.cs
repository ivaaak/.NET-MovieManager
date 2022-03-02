using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
using System.Diagnostics;

namespace MovieManager.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }


        //Review ?

        //edit lists?

        //Account settings/statistics?
    }
}