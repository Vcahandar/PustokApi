using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.DTOs.AuthDtos
{
    public record TokenResponseDto(string Token, DateTime expireDate);
  
}
