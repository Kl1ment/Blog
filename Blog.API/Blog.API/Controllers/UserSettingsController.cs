using Blog.API.Contracts;
using Blog.Application.Services;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{

    [ApiController]
    [Route("settings")]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserSettingsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<string>> UpdateUser(int id, [FromBody] SchemaRegister schemaRegister)
        {
            var user = UserModel.Create(id, schemaRegister.UserName);

            var result = await _userService.UpdateUser(schemaRegister.Email, schemaRegister.Password, user);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return Ok(id);
        }

    }
}
