using System.Net;

namespace Pustok.Business.Exceptions.BookExceptions
{
    public sealed class BookAlreadyExistException : Exception, IBaseException
    {
        public BookAlreadyExistException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; }
    }
}
