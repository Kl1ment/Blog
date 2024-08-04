using Blog.API.Models;
using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AdminController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoginModel>>> GetAllUser()
        {
            return await _loginService.GetAllUsers();
        }

    }
}
