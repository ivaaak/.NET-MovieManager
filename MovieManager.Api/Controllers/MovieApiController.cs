using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MovieManager.Services.ServicesContracts;
using MovieManager.Data.DataModels;

namespace MovieManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieApiController : ControllerBase
    {
        private readonly IGetFromDbService getFromDbService;

        public MovieApiController(IGetFromDbService getFromDbService)
        {
            this.getFromDbService = getFromDbService;
        }

        /// <summary>
        /// Get user playlists
        /// </summary>
        /// <param name="userName">The user's username</param>
        /// <returns></returns>
        [HttpPost]
        [Route("userPlaylists")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlaylists(string userName)
        {
            try
            {
                await getFromDbService.GetAllUserPlaylists(userName);
                return Ok();
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        /// <summary>
        /// Get all public playlists
        /// </summary>
        /// <param name="userName">The </param>
        /// <returns></returns>
        [HttpPost]
        [Route("publicPlaylists")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPublicLists()
        {
            try
            {
                await getFromDbService.GetAllPublicPlaylists();
                return Ok();
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        /// <summary>
        /// Get user's reviews/ratings
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns></returns>
        [HttpPost]
        [Route("movies")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserReviews(string username)
        {
            try
            {
                string id = await getFromDbService.GetUserIdFromUserName(username);
                await getFromDbService.GetAllUserReviews(username);
                return Ok();
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        //get movie stats?
    }
}