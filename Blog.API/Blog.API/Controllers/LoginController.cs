using Blog.API.Contracts;
using Blog.API.Models;
using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<string>> Login([FromQuery] SchemaLogin schemaLogin)
        {
            string error = await _loginService.Login(schemaLogin.Email, schemaLogin.Password);

            if (string.IsNullOrEmpty(error))
            {
                return Ok("Welcome");
            }

            return BadRequest(error);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] SchemaRegister schemaRegister)
        {
            var user = LoginModel.Register(
                schemaRegister.Email,
                schemaRegister.Password);

            if (user.newUser == null)
            {
                return BadRequest(user.error);
            }

            string error = await _loginService.Register(user.newUser);

            if (string.IsNullOrEmpty(error))
            {
                return Ok(user.newUser.Id);
            }

            return BadRequest(error);
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
