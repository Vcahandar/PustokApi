using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.UserExceptions
{
    public sealed class CreateUserFailException : Exception, IBaseException
    {

        public CreateUserFailException(IEnumerable<IdentityError> errors)
        {
            ErrorMessage = string.Join(" ", errors.Select(e => e.Description));
        }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }
    }
}
