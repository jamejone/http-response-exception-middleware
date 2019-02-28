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

                    if (ex != null)
                    {
                        if (ex.Error is IHttpException)
                        {
                            context.Response.StatusCode = (ex.Error as IHttpException).HttpStatusCode;
                            context.Response.ContentType = "application/json";

                            var responseObject = new {
                                message = ex.Error.Message
                            };

                            string responseBody = JsonConvert.SerializeObject(responseObject);

                            await context.Response.WriteAsync(responseBody.ToString());
                        }
                    }
                });
            });
        }
    }
}