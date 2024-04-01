using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.AuthorExceptions
{
    public class AuthorNotFoundByIdException : Exception,IBaseException
    {
        public AuthorNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }
    }
}
