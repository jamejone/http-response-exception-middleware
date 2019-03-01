using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace http_response_exception_middleware.Exceptions
{
    public class NotFoundException : Exception, IHttpException
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public int HttpStatusCode => StatusCodes.Status404NotFound;
    }
}