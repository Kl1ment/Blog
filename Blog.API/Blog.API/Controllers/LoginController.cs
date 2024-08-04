using Blog.API.Contracts;
using Blog.Application.Services;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("login")]
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
            var result = await _loginService.Login(schemaLogin.Email, schemaLogin.Password);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            HttpContext.Response.Cookies.Append("asp", result.Value);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] SchemaRegister schemaRegister)
        {
            var result = await _loginService.Register(schemaRegister.Email, schemaRegister.Password);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<int>> UpdateUser(int id, [FromBody] SchemaRegister schemaRegister)
        //{
        //    var userId = _loginService.UpdateUser(id, schemaRegister.Email, schemaRegister.Password);

        //    return Ok(userId);
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            return await _loginService.DeleteUser(id);
        }


    }
}
