using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.Exceptions.AuthorExceptions;
using Pustok.Business.Services.Interfaces;
using System.Net;

namespace Pustok.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync(search);
                return Ok(authors);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDto
                    (HttpStatusCode.InternalServerError, "Unexpected error occured"));
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
            }
            catch (AuthorNotFoundByIdException ex)
            {
                return NotFound(new ResponseDto(HttpStatusCode.NotFound, ex.Message));
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDto
                    (HttpStatusCode.InternalServerError, "Unexpected error occured"));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorPostDto authorPostDto)
        {
            try
            {
                await _authorService.CreateAuthorAsync(authorPostDto);
                return StatusCode((int)HttpStatusCode.Created,
                    new ResponseDto(HttpStatusCode.Created, "Authir successfully created"));
            }

            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseDto
                    (HttpStatusCode.InternalServerError, "Unexpected error occured"));
            }
        }
    }
}
