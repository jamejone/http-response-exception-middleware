using http_response_exception_middleware.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace http_response_exception_middleware.Middleware
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(options => 
            {
                options.Run(async context => {
                    var ex = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.ContentType = "application/json";

                    string responseMessage;
                    if (ex.Error is IHttpException)
                    {
                        context.Response.StatusCode = (ex.Error as IHttpException).HttpStatusCode;
                        responseMessage = ex.Error.Message;
                    }
                    else 
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        responseMessage = "internal server error :(";
                    }

                    var responseObject = new {
                        message = responseMessage
                    };
                    string responseBody = JsonConvert.SerializeObject(responseObject);

                    await context.Response.WriteAsync(responseBody.ToString());
                });
            });
        }
    }
}