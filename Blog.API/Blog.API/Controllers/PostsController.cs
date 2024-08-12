using Blog.API.Contracts;
using Blog.Application.Services;
using Blog.Core.Abstractions;
using Blog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<IPostModel>>> GetPostsByAuthorId([FromRoute] int id, [FromQuery] int page)
        {
            return await _postService.GetByAuthorId(id, page);
        }

        [HttpGet]
        public async Task<ActionResult<List<IPostModel>>> GetNewsFeed([FromQuery] int page)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            return await _postService.GetNewsFeed(userId, page);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePost(PostRequest postRequest)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

            var newPost = PostModel.Create(Guid.NewGuid(), id, postRequest.Title, postRequest.TextData);

            if (newPost.IsFailure)
            {
                return BadRequest(newPost.Error);
            }

            await _postService.Create(newPost.Value);

            return Ok(newPost.Value.Id);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeletePost([FromBody] Guid id)
        {
            return await _postService.Delete(id);
        }

    }
}
