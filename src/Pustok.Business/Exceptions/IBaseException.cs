using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions
{
    public interface IBaseException
    {
        HttpStatusCode  StatusCode { get; }
        string ErrorMessage { get; } 

    }
}
