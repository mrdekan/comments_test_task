using CommentsAPI.Interfaces;
using CommentsAPI.Models.DTO;
using CommentsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentsAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly AuthenticationService _authenticationService;

        public AuthController(IJwtService jwtService, AuthenticationService authenticationService)
        {
            _jwtService = jwtService;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginRequest)
        {
            var user = await _authenticationService.AuthenticateUserAsync(loginRequest.UserName, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerRequest)
        {
            // Call the RegisterUserAsync method
            var result = await _authenticationService.RegisterUserAsync(registerRequest.UserName, registerRequest.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User created successfully" });
            }
            else
            {
                return BadRequest(new { Errors = result.Errors });
            }
        }
    }
}
