namespace http_response_exception_middleware.Exceptions
{
    public interface IHttpException
    {
        int HttpStatusCode { get; }
    }
}