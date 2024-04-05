using Microsoft.AspNetCore.Mvc;
using Pustok.Business.DTOs.AuthDtos;
using Pustok.Business.Services.Interfaces;

namespace Pustok.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
                _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            return Ok(response);
        }
    }
}
