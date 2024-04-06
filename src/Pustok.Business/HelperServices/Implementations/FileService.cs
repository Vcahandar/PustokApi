using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pustok.Business.Exceptions.FileExceptions;
using Pustok.Business.Helpers.Extensions;
using Pustok.Business.HelperServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.HelperServices.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment= webHostEnvironment;
        }

        public async Task<string> FileUploadAsync(IFormFile file, string path, string type, int size)
        {
            if(!file.CheckFileType(type))
                throw new FileTypeException("This image is not a file");
            if (!file.CheckFileSize(size))
                throw new FileSizeException("Image size is large");

            string fileName = $"{Guid.NewGuid()}-{file.FileName}";
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,path,fileName);

            using FileStream stream = new FileStream(uploadPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;

        }
    }
}
