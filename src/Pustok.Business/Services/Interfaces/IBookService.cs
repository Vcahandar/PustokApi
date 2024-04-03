

using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Business.DTOs.BookDtos;

namespace Pustok.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookGetDto>> GetAllBooksAsync(string? search);
        Task<BookGetDto> GetBookByIdAsync(Guid id);
        Task CreateBookAsync(BookPostDto bookPostDto);
    }
}
