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
        [HttpGet("userPlaylists/{userName}")]
        public IActionResult GetPlaylists(string playlistTitle, string userId)
        {
            try
            {
                addToDbService.CreateCustomPlaylist(playlistTitle, userId);

                return Ok($"Added {playlistTitle} to user {userId}'s playlists");
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}