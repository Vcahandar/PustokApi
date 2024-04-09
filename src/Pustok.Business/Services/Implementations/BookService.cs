 using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.DTOs.BookDtos;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.Exceptions.BookExceptions;
using Pustok.Business.HelperServices.Interface;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Interfaces;
using System.Net;


namespace Pustok.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IFileService _fileService;

        public BookService(IBookRepository bookRepository, IMapper mapper,
                            IFileService fileService,
                            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<List<BookGetDto>> GetAllBooksAsync(string? search)
        {
            var books = await _bookRepository.GetFiltered(
                b => search != null ? b.Name.Contains(search) : true,
                isTracking: false,
                includes: new[] { "BookAuthors.Author", "BookImages" }
            ).ToListAsync();



            var booksDtos = _mapper.Map<List<BookGetDto>>(books);

            return booksDtos;

        }


        public async Task CreateBookAsync(BookPostDto bookPostDto)
        {
            Book book = _mapper.Map<Book>(bookPostDto);
            string mainImage = await _fileService.FileUploadAsync(bookPostDto.MainImage,
                Path.Combine("assets", "uploads", "images", "book-images"), "image/", 250);
            book.MainImage = mainImage;


            if (bookPostDto.Files is not null && bookPostDto.Files.Count > 0)
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



        public async Task<ResponseDto> UpdateBookAsync(BookPutDto bookPutDto)
        {
            var book = await _bookRepository.GetSingleAsync(b => b.Id ==bookPutDto.Id, "BookAuthors.Author", "BookImages");
            if (book is null)
                throw new BookNotFoundException($"No book found with the ID {bookPutDto.Id}");

            bool isExist = await _bookRepository.IsExistAsync(b => b.Name.ToLower().Trim() == bookPutDto.Name.ToLower().Trim() && b.Id != bookPutDto.Id);
            if (isExist)
                throw new BookAlreadyExistException($"A book with the title '{bookPutDto.Name}' already exists");

            string fileName = book.MainImage;

            if (bookPutDto.MainImage is not null)
            {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploads", "images", "book-images");
                    _fileService.DeleteFile(Path.Combine(path, book.MainImage));

                    fileName = await _fileService.FileUploadAsync(bookPutDto.MainImage,path, "image/", 250);
            }


            if (bookPutDto.Files is not null && bookPutDto.Files.Count > 0)
            {
                List<BookImage> files = new List<BookImage>();

                foreach (var file in bookPutDto.Files)
                {
                    string image = await _fileService.FileUploadAsync(file,
                   Path.Combine("assets", "uploads", "images", "book-images"), "image/", 250);
                    files.Add(new BookImage { Image = image });
                };

                book.BookImages = files;
            }



            var updatedBook = _mapper.Map(bookPutDto, book);
            book.MainImage = fileName;

            _bookRepository.Update(updatedBook);

            await _bookRepository.SaveAsync();

            return new(HttpStatusCode.OK, "The book was successfully updated");

        }

        public async Task<ResponseDto> DeleteBookAsync(Guid Id)
        {
            var book = await _bookRepository.GetSingleAsync(b => b.Id == Id);
            if (book is null)
                throw new BookNotFoundException($"The book with ID {Id} was not found");

            _bookRepository.SoftDelete(book);
            await _bookRepository.SaveAsync();

            return new(HttpStatusCode.OK, "The book has been successfully deleted");
        }


        public async Task<BookGetDto> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetSingleAsync(b => b.Id == id, "BookAuthors.Author", "BookImages");

            if (book is null)
                throw new BookNotFoundException($"The book with ID {id} was not found");

            var bookDto = _mapper.Map<BookGetDto>(book);
            return bookDto;
        }


    }
}
