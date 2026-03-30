using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModel.DTOs;

namespace QuantityMeasurementWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var result = _service.Register(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var token = _service.Login(dto);
            return Ok(new
        {
            message = "Login successful",
            token
            });
        }

        [HttpPost("google")]
        public IActionResult GoogleLogin(GoogleLoginDTO dto)
        {
            var token = _service.GoogleLogin(dto.IdToken);
            return Ok(new { token });
        }
    }
}