using ECommerce.Core.Interfaces;
using ECommerce.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

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
    }
}

