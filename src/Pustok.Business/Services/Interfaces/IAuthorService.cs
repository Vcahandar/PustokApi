using Pustok.Business.DTOs.AuthorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorGetDto>> GetAllAuthorsAsync(string? search);
        Task<AuthorGetDto> GetAuthorByIdAsync(Guid id);
        Task CreateAuthorAsync(AuthorPostDto authorPostDto);
    }
}
