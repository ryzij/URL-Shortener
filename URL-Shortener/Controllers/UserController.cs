using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTO;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDTO dto)
        {
            var jwt = await _userService.RegisterAsync(dto.Name, dto.Email, dto.Password);
            return Ok(jwt);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDTO dto)
        {
            var jwt = await _userService.LoginAsync(dto.Email, dto.Password);
            return Ok(jwt);
        }
    }
}
