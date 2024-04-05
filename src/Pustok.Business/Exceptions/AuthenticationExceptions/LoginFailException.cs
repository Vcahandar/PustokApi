using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.AuthenticationExceptions
{
    public sealed class LoginFailException : Exception, IBaseException
    {
        public LoginFailException(string message) : base(message)
        {
            ErrorMessage= message;
        }
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage { get; }
    }
}
