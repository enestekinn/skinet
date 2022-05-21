using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Erros;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;


//RequestDelegate can process http request.
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context){
            try{
                // if there is no exception request moves on to its next stage
await  _next(context);


            }
            catch(Exception ex){
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                var response = _env.IsDevelopment()
                ?
                // Development  
                new ApiException(
                    (int)HttpStatusCode.InternalServerError,
                    ex.Message,
                    ex.StackTrace.ToString())
                    :
                    //  Production
                    new ApiException((int)HttpStatusCode.InternalServerError);

// jsonda bazi degerler StatusCode Pascal case seklinde gozukuyor onu statusCode sekline donusturecegiz.
                    var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                    var json = JsonSerializer.Serialize(response,options);
                    await context.Response.WriteAsync(json);
            }
        }
}
}