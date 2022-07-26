using Microsoft.AspNetCore.Mvc;
using MovieManager.Core.Contracts;

namespace MovieManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IAddToDbService addToDbService;

        public PostController(IAddToDbService addToDbService)
        {
            this.addToDbService = addToDbService;
        }

        /// <summary>
        /// Create a playlist for a user
        /// </summary>
        /// <param name="userId">The user's ID!</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePlaylist(string playlistTitle, string userId)
        {
            try
            {
                addToDbService.CreateCustomPlaylist(playlistTitle, userId);

                return Ok($"Created the playlist {playlistTitle} for user {userId}");
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}