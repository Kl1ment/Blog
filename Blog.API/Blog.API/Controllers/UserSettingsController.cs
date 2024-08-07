using Blog.API.Contracts;
using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{

    [ApiController]
    [Route("settings")]
    public class UserSettingsController : ControllerBase
    {

        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public UserSettingsController(
            ILoginService loginService,
            IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;

        }



        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<int>> UpdateUser(int id, [FromBody] SchemaRegister schemaRegister)
        //{
        //    var userId = await _loginService.UpdateUser(id, schemaRegister.Email, schemaRegister.Password);

        //    return Ok(userId);
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return await _loginService.DeleteUser(id);
        }

    }
}
