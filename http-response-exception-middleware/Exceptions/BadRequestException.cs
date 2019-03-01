using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace http_response_exception_middleware.Exceptions
{
    public class BadRequestException : Exception, IHttpException
    {
        public BadRequestException() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public int HttpStatusCode => StatusCodes.Status400BadRequest;
    }
}