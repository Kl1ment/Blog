using Blog.API.Contracts;
using Blog.Application.Services;
using Blog.Core.Abstractions;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("{id:int}")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IPostModel>>> GetPostsByAuthorId([FromRoute] int id)
        {
            return await _postService.GetByAuthorId(id);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePost(HttpContext context, int id, PostRequest postRequest)
        {
            

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
