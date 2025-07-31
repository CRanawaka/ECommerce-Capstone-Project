using ECommerce.Core.Interfaces;
using ECommerce.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            try
            {
                var newUser = await _authService.RegisterAsync(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password);

                return CreatedAtAction(nameof(Register), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user's ID
            var userEmail = User.FindFirstValue(ClaimTypes.Email);       // Gets the user's email

            if (userId == null)
            {
                return Unauthorized();
            }

            // You can now use this ID to fetch more user details from the database
            return Ok(new { Id = userId, Email = userEmail });
        }
    }
}

