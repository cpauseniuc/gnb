using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.ErrorHandling
{
    public class ApiError
    {
        public ApiError(string error, Exception ex) { Error = error; }
        public string Error { get; set; }
        public Exception Exception { get; set; }
    }
}
