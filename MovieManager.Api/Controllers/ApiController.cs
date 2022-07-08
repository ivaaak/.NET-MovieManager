using Microsoft.AspNetCore.Mvc;
using MovieManager.Api.ApiServices;
using System.Text.Json;

namespace MovieManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IGetFromDbService getFromDbService;

        public ApiController(IGetFromDbService getFromDbService)
        {
            this.getFromDbService = getFromDbService;
        }

        /// <summary>
        /// Get user playlists
        /// </summary>
        /// <param name="userName">The username!</param>
        /// <returns></returns>
        [HttpGet("userPlaylists/{userName}")]
        public async Task<IActionResult> GetPlaylists(string userName)
        {
            try
            {
                var resultUserPlaylists = await getFromDbService.GetAllUserPlaylists(userName);

                string jsonResult = JsonSerializer.Serialize(resultUserPlaylists);

                return Ok(jsonResult);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        /// <summary>
        /// Get all public playlists
        /// </summary>
        /// <returns></returns>
        [HttpGet("publicPlaylists")]
        public async Task<IActionResult> GetPublicLists()
        {
            try
            {
                var resultPublicPlaylists = await getFromDbService.GetAllPublicPlaylists();

                string jsonResult = JsonSerializer.Serialize(resultPublicPlaylists);

                return Ok(jsonResult);
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
        [HttpGet("userReviews/{username}")]
        public async Task<IActionResult> GetUserReviews(string username)
        {
            try
            {
                string id = await getFromDbService.GetUserIdFromUserName(username);
                var resultUserReviews = await getFromDbService.GetAllUserReviews(username);

                string jsonResult = JsonSerializer.Serialize(resultUserReviews);

                return Ok(jsonResult);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }


        /// <summary>
        /// Get movie data from id
        /// </summary>
        /// <param id="movieId">The movie's id used by the TMDB Api</param>
        /// <returns></returns>
        [HttpGet("movieIdInt/{id}")]
        public async Task<IActionResult> GetMovieDataFromId(int id)
        {
            try
            {
                var resultMovie = await getFromDbService.GetMovieDataFromId(id);

                string jsonResult = JsonSerializer.Serialize(resultMovie);

                return Ok(jsonResult);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }


        /// <summary>
        /// Get movie data from name
        /// </summary>
        /// <param id="title">The movie's id used by the TMDB Api</param>
        /// <returns></returns>
        [HttpGet("movieIdString/{title}")]
        public async Task<IActionResult> GetMovieDataFromTitle(string title)
        {
            try
            {
                var resultMovie = await getFromDbService.GetMovieDataFromName(title);

                string jsonResult = JsonSerializer.Serialize(resultMovie);

                return Ok(jsonResult);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }
    }
}