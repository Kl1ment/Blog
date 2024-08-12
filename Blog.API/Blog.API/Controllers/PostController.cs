using Blog.API.Contracts;
using Blog.Application.Services;
using Blog.Core.Abstractions;
using Blog.Core.Models;
using Blog.Infrastucture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<IPostModel>>> GetPostsByAuthorId([FromRoute] int id)
        {
            return await _postService.GetByAuthorId(id);
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
