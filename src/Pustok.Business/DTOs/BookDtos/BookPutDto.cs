using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.DTOs.BookDtos
{
    public record BookPutDto(Guid Id, string Name, string Description, decimal Price,
                             int DiscountPercent, int PageCount, int StockCount, int Rating,
                             ICollection<Guid> AuthorsIds, IFormFile MainImage,
                             ICollection<IFormFile>? Files
                             );

}
