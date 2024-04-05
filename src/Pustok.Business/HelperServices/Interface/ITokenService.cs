using Pustok.Business.DTOs.AuthDtos;
using Pustok.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.HelperServices.Interface
{
    public interface ITokenService
    {
        TokenResponseDto GenerateToken(AppUser user);
    }
}
