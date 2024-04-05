using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.Exceptions.AuthorExceptions;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Interfaces;
using System.Net;

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

        public async Task<ResponseDto> UpdateAuthorAsync(AuthorPutDto authorPutDto)
        {
            bool isExist = await _authorRepository.IsExistAsync(b => b.Fullname.ToLower().Trim() == authorPutDto.Fullname.ToLower().Trim() && b.Id != authorPutDto.Id);
            if (isExist) throw new AuthorAlreadyExistException($"An author with the name '{authorPutDto.Fullname}' already exists.");
            var author = await _authorRepository.GetSingleAsync(b => b.Id == authorPutDto.Id);
            if (author is null) throw new AuthorNotFoundByIdException($"Author not found with ID {authorPutDto.Id}");

            var updatedAuthor = _mapper.Map(authorPutDto, author);


            _authorRepository.Update(updatedAuthor);
            await _authorRepository.SaveAsync();

            return new(HttpStatusCode.OK, "Author has been successfully updated");

        }

        public async Task<ResponseDto> DeleteAuthorAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id) ?? throw new AuthorNotFoundByIdException($"No author found with ID {id}");
            _authorRepository.SoftDelete(author);
            await _authorRepository.SaveAsync();

            return new ResponseDto(HttpStatusCode.OK, "Author has been successfully deleted");

        }
    }
}
