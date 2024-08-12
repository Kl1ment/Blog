using Blog.API.Contracts;
using Blog.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserActionController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserActionController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<List<UserResponse>> GetUsers([FromQuery] int page)
        {
            var users = await _userService.GetUsers(page);

            return users.Select(u => new UserResponse(u.Id, u.UserName)).ToList();
        }

        [HttpPut("{authorId:int}")]
        public async Task<ActionResult> Subscribe([FromRoute] int authorId)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            var result = await _userService.Subscribe(userId, authorId);

            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("subscriptions")]
        public async Task<ActionResult> Unsubscribe([FromBody] int authorId)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            var result = await _userService.Unsubscribe(userId, authorId);

            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("subscriptions")]
        public async Task<ActionResult<List<UserResponse>>> GetSubscriptions([FromQuery] int userId)
        {
            var subscriptions = await _userService.GetSubscriptions(userId);

            if (subscriptions == null)
            {
                return BadRequest();
            }

            return Ok(subscriptions.Select(u => new UserResponse(u.Id, u.UserName)).ToList());
        }

        [HttpGet("followers")]
        public async Task<ActionResult<List<UserResponse>>> GetFollowers([FromQuery] int userId)
        {
            var followers = await _userService.GetFollowers(userId);

            if (followers == null)
            {
                return BadRequest();
            }

            return Ok(followers.Select(u => new UserResponse(u.Id, u.UserName)).ToList());
        }
    }
}
