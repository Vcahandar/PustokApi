using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.DTOs.AuthorDtos
{
    public record AuthorGetDto(Guid Id, string Fullname);

}
