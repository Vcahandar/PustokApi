using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Helpers.Extensions
{
    public static class FileExtension
    {
        public static bool CheckFileType(this IFormFile file, string type)
           => file.ContentType.Contains(type);

        public static bool CheckFileSize(this IFormFile file, int size)
            =>file.Length/1024 < size;


    }
}
