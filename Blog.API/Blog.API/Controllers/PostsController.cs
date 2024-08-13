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

        private int UserId => Convert.ToInt32(HttpContext.User.FindFirstValue("userId"));

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
            return await _postService.GetNewsFeed(UserId, page);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePost(PostRequest postRequest)
        {
            var newPost = PostModel.Create(Guid.NewGuid(), UserId, postRequest.Title, postRequest.TextData);

            if (newPost.IsFailure)
            {
                return BadRequest(newPost.Error);
            }

            await _postService.Create(newPost.Value);

            return Ok(newPost.Value.Id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePost(PostRequest postRequest)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePost([FromBody] Guid id)
        {
            var result = await _postService.Delete(UserId, id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

    }
}
