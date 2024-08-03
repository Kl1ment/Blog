using Blog.API.Contracts;
using Blog.API.Models;
using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("controller")]
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
            if (await _loginService.Login(schemaLogin.Email, schemaLogin.Password))
            {
                return Ok("Welcome");
            }

            return BadRequest("Неверный логин или пароль");
        }

        [HttpPost]
        public async Task<ActionResult<int>> Register([FromBody] SchemaRegister schemaRegister)
        {
            var user = LoginModel.Register(
                schemaRegister.Email,
                schemaRegister.Password);

            if (user.newUser == null)
            {
                return BadRequest(user.error);
            }

            return await _loginService.Register(user.newUser);
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
