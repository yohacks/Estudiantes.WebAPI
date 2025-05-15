namespace Utilitary.Core.Common.Utilities
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IO;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilitary.Domine.Common;

    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly InternalServices _internalService;
        private readonly IWebHostEnvironment _enviroment;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingMiddleware(RequestDelegate next, InternalServices internalService, IWebHostEnvironment env, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _internalService = internalService;
            _enviroment = env;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_configuration["LoggingMiddleware"] == "true" && context.Request.Path.ToString().Contains("estudiantes"))
            {
                LogMiddleware request = await FormatRequest(context.Request);

                var originalBodyStream = context.Response.Body;

                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                LogResponse response = await FormatResponse(context.Response);

                await responseBody.CopyToAsync(originalBodyStream);

                request.StatusCode = response.StatusCode.ToString();
                request.Response = response.Response;
                request.UserId = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Headers["EstudiantesUserId"]);
                request.DateRegister = DateTime.Now;

                new Thread(x => _internalService.SaveLogging(request)).Start();
            }
            else
            {
                await _next(context);
            }
        }

        private async Task<LogMiddleware> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await request.Body.CopyToAsync(requestStream);
            request.Body.Position = 0;

            LogMiddleware newLogRequest = new LogMiddleware
            {
                Environment = _enviroment.EnvironmentName.ToString() + " - " + request.HttpContext.Connection.RemoteIpAddress.ToString(),
                Method = request.Method,
                Host = request.Host.ToString(),
                Path = request.Path,
                QueryString = request.QueryString.ToString(),
                Request = ReadStreamInChunks(requestStream)
            };

            return newLogRequest;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        private static async Task<LogResponse> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            LogResponse newLogResponse = new LogResponse
            {
                StatusCode = response.StatusCode.ToString(),
                Response = text
            };

            return newLogResponse;
        }
    }
}
