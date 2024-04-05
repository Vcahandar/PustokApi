using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pustok.Business.DTOs.AuthDtos;
using Pustok.Business.HelperServices.Interface;
using Pustok.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.HelperServices.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public TokenResponseDto GenerateToken(AppUser user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email), 
                new Claim(ClaimTypes.NameIdentifier,user.Id), 
            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration["Jwt:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256);

            //var expires = DateTime.UtcNow.AddHours(2);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _configuration["Jwt:Issuer"],
                audience : _configuration["Jwt:Audience"],
                claims : claims,
                signingCredentials :signingCredentials,
                notBefore:DateTime.UtcNow,
                expires:DateTime.UtcNow.AddHours(2)
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token=jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return  new TokenResponseDto(token, jwtSecurityToken.ValidTo);
        }

    }
}
