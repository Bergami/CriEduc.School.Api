using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.Shared.Enum;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Text.Json;

namespace CriEduc.School.UseCase
{
    public abstract class UseCaseBase<TRequest, TResponse>
    {
        protected readonly ILogger _logger;
        protected readonly IValidator<TRequest> _validator;
        protected readonly Tracer _tracer;

        protected UseCaseBase(ILogger logger, IValidator<TRequest> validator, Tracer tracer)
        {
            _logger = logger;
            _validator = validator;
            _tracer = tracer;
        }

        public async Task<UseCaseResponse<TResponse>> Execute(TRequest request)
        {
            var useCaseName = this.GetType().Name; 

            var span = _tracer.StartSpan($"{useCaseName} Execution", SpanKind.Internal);

            try
            {
                await _validator.ValidateAndThrowAsync(request);
                span.AddEvent("Validação concluída");

                var response = await ExecuteUseCaseAsync(request);
                span.AddEvent("Execução do caso de uso concluída");

                return response;
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x => new ErrorMessage(x.ErrorCode, x.ErrorMessage));

                _logger.LogInformation(e.Message, e, errors);

                span.RecordException(e);
                span.SetStatus(Status.Error.WithDescription(e.Message));

                return new UseCaseResponse<TResponse>().SetBadResquest(Message.ValidateRequest, errors);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                
                span.RecordException(e);
                span.SetStatus(Status.Error.WithDescription(e.Message));

                return new UseCaseResponse<TResponse>().SetStatus(UseCaseResponseKind.InternalServerError, Message.InternalServerErrorGeneralMessage);
            }
            finally
            {                
                span.SetAttribute("usecase.name", useCaseName);
                
                if (request != null)
                {
                    var requestJson = JsonSerializer.Serialize(request);

                    span.SetAttribute("usecase.request", requestJson);
                }
            
                span.End();
            }
        }

        protected abstract Task<UseCaseResponse<TResponse>> ExecuteUseCaseAsync(TRequest request);
    }

}
