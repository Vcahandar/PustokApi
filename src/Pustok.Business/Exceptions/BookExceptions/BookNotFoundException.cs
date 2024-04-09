using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.BookExceptions
{
    public sealed class BookNotFoundException : Exception, IBaseException
    {
        public BookNotFoundException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }
    }
}
