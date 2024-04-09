using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.DTOs.BookDtos;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.Services.Interfaces;
using System.Net;

namespace Pustok.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            return Ok(await _bookService.GetAllBooksAsync(search));
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromForm]BookPostDto bookPostDto)
        {
            await _bookService.CreateBookAsync(bookPostDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(HttpStatusCode.Created,
                                                         "Book successfully created"));
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] BookPutDto bookPutDto)
        {
                var response = await _bookService.UpdateBookAsync(bookPutDto);
                return StatusCode((int)response.StatusCode, response.Message);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _bookService.DeleteBookAsync(id);

            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}
