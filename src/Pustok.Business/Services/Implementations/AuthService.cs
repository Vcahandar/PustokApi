using Microsoft.AspNetCore.Identity;
using Pustok.Business.DTOs.AuthDtos;
using Pustok.Business.Exceptions.AuthenticationExceptions;
using Pustok.Business.HelperServices.Interface;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Entities.Identity;

namespace Pustok.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _sinInManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> sinInManager, ITokenService tokenService)
        {
            _sinInManager = sinInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
        {
         var user =  await _userManager.FindByNameAsync(loginDto.Username);
            if (user is null)
            {
                throw new LoginFailException("Username or password incorrect"); 
            }

            var singInManager =  await  _sinInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (!singInManager.Succeeded)
            {
                throw new LoginFailException("Username or password incorrect");
            }

            //token
            var tokenResponse = _tokenService.GenerateToken(user);

            return tokenResponse;
        }
    }
}
