using System.Net;
using Shared.Entities.Common;
using Shared.Extentions;
using Shared.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Logging
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;
        public static readonly List<string> RequestHeaders = new();
        public static readonly List<string> ResponseHeaders = new();

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var uniqueRequestHeaders = context.Request.Headers
                    .Where(x => RequestHeaders.All(r => r != x.Key))
                    .Select(x => x.Key).ToList();
                RequestHeaders.AddRange(uniqueRequestHeaders);

                _logger.LogInformation("Request Header :{@requestHeader}", RequestHeaders);

                await LogRequest(context);

                //await _next.Invoke(context);

                await LogResponse(context);
                var uniqueResponseHeaders = context.Response.Headers
                    .Where(x => ResponseHeaders.All(r => r != x.Key))
                    .Select(x => x.Key).ToList();
                ResponseHeaders.AddRange(uniqueResponseHeaders);

                _logger.LogInformation("Response Header :{@responseHeader}", ResponseHeaders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hata Oluştu : {@Exception}", ErrorHelper.GetExceptionString(ex));
                await HandleExceptionAsync(context);
            }
            
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {body}");
            context.Request.Body.Position = 0;
        }
        
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                       $"Schema:{context.Request.Scheme} " +
                                       $"Host: {context.Request.Host} " +
                                       $"Path: {context.Request.Path} " +
                                       $"QueryString: {context.Request.QueryString} " +
                                       $"Response Body: {text}");

                await responseBody.CopyToAsync(originalBodyStream);
            }

            
        }

        private Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new ServiceResponse(Constants.ErrorCode, Constants.ExceptionErrorMessage).ToString());
        }
    }
}
