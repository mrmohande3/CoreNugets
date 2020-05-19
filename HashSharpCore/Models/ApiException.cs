using System;
using System.Collections.Generic;
using System.Text;
using HashSharpCore.Filters;

namespace HashSharpCore.Models
{
    public class ApiException : Exception
    {
        public ApiResultStatusCode StatusCode { get; set; }

        public ApiException()
        {
            StatusCode = ApiResultStatusCode.ServerError;
        }
        public ApiException(string message) : base(message)
        {
            StatusCode = ApiResultStatusCode.ServerError;
        }

        public ApiException(string message, ApiResultStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
