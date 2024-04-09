using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pustok.Business.Exceptions.FileExceptions;
using Pustok.Business.Helpers.Extensions;
using Pustok.Business.HelperServices.Interface;
using F = System.IO;



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

        //public static void DeleteFile(string[] path)
        //{
        //    var oldPath = Path.Combine(path);

        //    if (File.Exists(oldPath))
        //        File.Delete(oldPath);
        //}

        //public static void DeleteFile(string path)
        //{
        //    if (File.Exists(path))
        //        File.Delete(path);
        //}


        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


    }
}
