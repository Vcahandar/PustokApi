using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Business.DTOs.CommonDtos;

namespace Pustok.Business.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorGetDto>> GetAllAuthorsAsync(string? search);
        Task<AuthorGetDto> GetAuthorByIdAsync(Guid id);
        Task CreateAuthorAsync(AuthorPostDto authorPostDto);
        Task<ResponseDto> UpdateAuthorAsync(AuthorPutDto authorPutDto);
        Task<ResponseDto> DeleteAuthorAsync(Guid id);
    }
}
