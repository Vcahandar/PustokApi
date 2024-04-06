 using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.DTOs.BookDtos;
using Pustok.Business.HelperServices.Interface;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public BookService(IBookRepository bookRepository, IMapper mapper, IFileService fileService)
        {
            _bookRepository=bookRepository;
            _mapper=mapper;
            _fileService = fileService;
        }


        public async Task<List<BookGetDto>> GetAllBooksAsync(string? search)
        {
            var books =  await _bookRepository.GetFiltered(b => search != null ?
            b.Name.Contains(search) : true,includes: "BookAuthors.Author").ToListAsync();

            var booksDtos = _mapper.Map<List<BookGetDto>>(books);

            return booksDtos;
           
        }


        public async Task CreateBookAsync(BookPostDto bookPostDto)
        {
           Book book = _mapper.Map<Book>(bookPostDto);
           string mainImage =  await _fileService.FileUploadAsync(bookPostDto.MainImage,
               Path.Combine("assets","uploads","images","book-images"),"image/", 250);
            book.MainImage = mainImage;


            if (bookPostDto.Files is not null && bookPostDto.Files.Count>0)
            {
                List<BookImage> files = new List<BookImage>();

                foreach (var file in bookPostDto.Files)
                {
                    string image = await _fileService.FileUploadAsync(file,
                   Path.Combine("assets", "uploads", "images", "book-images"), "image/", 250);
                    files.Add(new BookImage { Image = image });
                };

                book.BookImages = files;
            }



            await _bookRepository.CreateAsync(book);
            await _bookRepository.SaveAsync();

        }

        public Task<BookGetDto> GetBookByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
