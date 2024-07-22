using CriEduc.School.Border.Shared;
using CriEduc.School.Border.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Serilog;
using System.Reflection.PortableExecutable;

namespace CriEduc.School.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSucess = false);
    }
    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string path;
        private IHeaderDictionary? _headers;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            _headers = accessor.HttpContext?.Response.Headers;

            path = accessor.HttpContext.Request.Path;
        }

        public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSucess = false)
        {
            
            if (response is null)
                return BuildeError(new[] { new ErrorMessage("0000", "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);


            if (response.Sucess())
                return noContentIfSucess
                    ? new NoContentResult()
                    : BuildSucessResult(response.Result!, response.Status,  response.Headers);

            if (response.Result is not null)
                return BuildeError(response.Result, response.Status);

            var error = GetError(response);

            return BuildeError(error, response.Status);
        }

        private IEnumerable<ErrorMessage> GetError<T>(UseCaseResponse<T> response)
        {
            var hasError = response.Erros?.Any();
            var errorResult = hasError is not null ? response.Erros : new[]
            {
                new ErrorMessage("0000", response.ErrorMessage ?? "Unknown Error")
            };

            return errorResult!;
        }

        private ObjectResult BuildeError(object data, UseCaseResponseKind status)
        {
            var httpStatus = GerErrorHttpStatusCode(status);

            if (httpStatus == HttpStatusCode.InternalServerError)
            {
                Log.Error($"[Error] {path} ({{@data}})", data);
            }

            return new ObjectResult(data)
            {
                StatusCode = (int)httpStatus,
            };
        }
      
        private IActionResult BuildSucessResult(object data, UseCaseResponseKind status, Dictionary<string, string> headers = null)
        {
            ExistsHeaders(headers);

            return status switch
            {
                UseCaseResponseKind.NoContent => new NoContentResult(),
                _ => new OkObjectResult(data),
            };
        }

        private void ExistsHeaders(Dictionary<string, string> headers)
        {
            if (headers != null)
                foreach (var header in headers)
                    _headers!.Add(header.Key, header.Value);
        }

        private static HttpStatusCode GerErrorHttpStatusCode(UseCaseResponseKind status)
        {
           return status switch
           {
               UseCaseResponseKind.NotFound => HttpStatusCode.NotFound,
               UseCaseResponseKind.BadRequest => HttpStatusCode.BadRequest,
               UseCaseResponseKind.BadGateway => HttpStatusCode.BadGateway,
               _ => HttpStatusCode.InternalServerError,
           };
        }

       
    }    
}
