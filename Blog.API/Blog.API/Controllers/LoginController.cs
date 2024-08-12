using Blog.API.Contracts;
using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] SchemaLogin schemaLogin)
        {
            var token = await _loginService.Login(schemaLogin.Email, schemaLogin.Password);

            if (token.IsFailure)
            {
                return BadRequest(token.Error);
            }

            HttpContext.Response.Cookies.Append("asp", token.Value);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] SchemaRegister schemaRegister)
        {
            var result = await _loginService.Register(schemaRegister.UserName, schemaRegister.Email, schemaRegister.Password);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
