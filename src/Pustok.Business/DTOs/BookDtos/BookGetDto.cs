using Pustok.Business.DTOs.AuthorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.DTOs.BookDtos
{
    public record BookGetDto(string Name,string Description,decimal Price,
                             int DiscountPercent, int PageCount, int StockCount, int Rating,
                             ICollection<AuthorGetDto> Authors);
  
}
