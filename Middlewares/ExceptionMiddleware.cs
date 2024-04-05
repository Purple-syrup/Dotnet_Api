using System.Net;
using Dotnet_Api.Errors;


namespace Dotnet_Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
                                    ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception ex)
            {
                ApiError response;
                HttpStatusCode statusCode=HttpStatusCode.InternalServerError;
                string message;
                var exceptionType= ex.GetType();
                if(exceptionType==typeof(UnauthorizedAccessException)){
                    statusCode=HttpStatusCode.Forbidden;
                    message= "You are not authorized";
                }else{
                    statusCode=HttpStatusCode.InternalServerError;
                    message= "SomeUnknown Error Occured";
                }
                if (_env.IsDevelopment())
                {
                    response= new ApiError((int)statusCode, ex.Message,ex.StackTrace);
                }else{

                    response= new ApiError((int)statusCode, message);
                }
                _logger.LogError(ex,ex.Message);
                context.Response.StatusCode=(int)statusCode;
                context.Response.ContentType="application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}