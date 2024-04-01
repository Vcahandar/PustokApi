using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.AuthorExceptions
{
    public class AuthorNotFoundByIdException : Exception
    {
        public AuthorNotFoundByIdException(string message) : base(message)
        {

        }
    }
}
