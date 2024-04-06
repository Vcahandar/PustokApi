using System.Net;


namespace Pustok.Business.Exceptions.FileExceptions
{
    public sealed class FileTypeException : Exception, IBaseException
    {

        public FileTypeException(string message) :base(message) 
        {
            ErrorMessage = message;
        }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get;}
    }
}
