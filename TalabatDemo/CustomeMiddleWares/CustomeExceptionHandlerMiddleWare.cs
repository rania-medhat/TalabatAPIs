using System.Text.Json;
using Shared.DTOs.ErrorModels;

namespace TalabatDemo.CustomeMiddleWares
{
    public class CustomeExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandlerMiddleWare> _logger;

        public CustomeExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<CustomeExceptionHandlerMiddleWare> logger)
        {
            //create and assign field
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
           
            try
            {
                await _next.Invoke(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                //set status code for response
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //set content type for response
                //httpContext.Response.ContentType = "application/json";
                //create response object
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };
                //return response object as json
                //var responseToReturn=JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsJsonAsync(response);

            }
        }

    }
}
