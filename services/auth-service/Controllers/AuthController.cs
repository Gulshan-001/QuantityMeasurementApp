using AuthService.Services;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            var result = _service.Register(dto);
            return Ok(new { success = true, message = result });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var token = _service.Login(dto);
            return Ok(new { message = "Login successful", token });
        }

        [HttpPost("google")]
        public IActionResult GoogleLogin([FromBody] GoogleLoginDTO dto)
        {
            var token = _service.GoogleLogin(dto.IdToken);
            return Ok(new { token });
        }

        [HttpGet("test-db")]
        public IActionResult TestDb()
        {
            try
            {
                var userCount = _service.GetUserCount();
                return Ok(new { success = true, userCount, message = "Auth service DB verified." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
