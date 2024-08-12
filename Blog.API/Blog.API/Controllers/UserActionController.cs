using Blog.API.Contracts;
using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserActionController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserActionController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<ActionResult> Subscribe([FromBody] int authorId)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            var result = await _userService.Subscribe(userId, authorId);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("subscriptions")]
        public async Task<ActionResult> Unsubscribe([FromBody] int authorId)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            var result = await _userService.Unsubscribe(userId, authorId);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("subscriptions")]
        public async Task<ActionResult<List<UserResponse>>> GetSubscriptions([FromQuery] int userId)
        {
            var subscriptions = await _userService.GetSubscriptions(userId);

            if (subscriptions == null)
            {
                return BadRequest();
            }

            return Ok(subscriptions);
        }

        [HttpGet("followers")]
        public async Task<ActionResult<List<UserResponse>>> GetFollowers([FromQuery] int userId)
        {
            var subscriptions = await _userService.GetFollowers(userId);

            if (subscriptions == null)
            {
                return BadRequest();
            }

            return Ok(subscriptions);
        }
    }
}
