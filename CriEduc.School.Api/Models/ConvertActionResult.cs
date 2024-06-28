using CriEduc.School.Border.Shared;
using CriEduc.School.Border.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Serilog;

namespace CriEduc.School.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSucess = false);
    }
    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            path = accessor.HttpContext.Request.Path;
        }

        public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSucess = false)
        {
            if (response is null)
                return BuildeError(new[] { new ErrorMessage("0000", "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);


            if (response.Sucess())
                return noContentIfSucess
                    ? new NoContentResult()
                    : BuildSucessResult(response.Result!, response.Status);

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

       private static IActionResult BuildSucessResult(object data, UseCaseResponseKind status)
       {
            return status switch
            {
                UseCaseResponseKind.NoContent => new NoContentResult(),               
                _ => new OkObjectResult(data),
            };
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
