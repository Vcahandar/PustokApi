using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Business.Exceptions.AuthorExceptions;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Interfaces;


namespace Pustok.Business.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }


        public async Task<List<AuthorGetDto>> GetAllAuthorsAsync(string? search)
        {
            var authors = await _authorRepository.GetFiltered(a => search != null ? a.Fullname.Contains
            (search) : true).ToListAsync();

            var authorDtos = _mapper.Map<List<AuthorGetDto>>(authors);
            return authorDtos;

        }

        public async Task<AuthorGetDto> GetAuthorByIdAsync(Guid id)
        {
            var aouthor = await _authorRepository.GetSingleAsync(a => a.Id == id);

            if (aouthor is null)
            {
                throw new AuthorNotFoundByIdException($"Author not found by id : {id}");
            }

            var authorDto = _mapper.Map<AuthorGetDto>(aouthor);
            return authorDto;
        }

        public async Task CreateAuthorAsync(AuthorPostDto authorPostDto)
        {
            var newAuthor = _mapper.Map<Author>(authorPostDto);

            await _authorRepository.CreateAsync(newAuthor);
            await _authorRepository.SaveAsync();
        }
    }
}
