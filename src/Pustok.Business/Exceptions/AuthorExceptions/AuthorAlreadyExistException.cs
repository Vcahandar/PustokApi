using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.AuthorExceptions
{
    public sealed class AuthorAlreadyExistException : Exception, IBaseException
    {

        public AuthorAlreadyExistException(string message) :base(message)
        {
            ErrorMessage = message;
        }
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; set; }
    }
}
