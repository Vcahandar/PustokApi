

using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Business.DTOs.BookDtos;
using Pustok.Business.DTOs.CommonDtos;

namespace Pustok.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookGetDto>> GetAllBooksAsync(string? search);
        Task<BookGetDto> GetBookByIdAsync(Guid id);
        Task CreateBookAsync(BookPostDto bookPostDto);
        Task<ResponseDto> UpdateBookAsync(BookPutDto bookPutDto);

    }
}
