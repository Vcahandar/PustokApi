using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.DTOs.BookDtos
{
    public record BookImageGetDto(string Image, Guid BookId);
}
