using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System.Text.Json;

namespace CriEduc.School.UseCase.Teacher;

public class GetTeacherUseCase : IGetTeacherUseCase
{    
    private readonly ITeachersRepository _teachersRepository;
    private readonly Tracer _tracer;
   
    private readonly ILogger<CreateTheacherUseCase> _logger;
    public GetTeacherUseCase(ITeachersRepository teachersRepository, 
                            ILogger<CreateTheacherUseCase> logger,
                            Tracer tracer)
    {       
        _teachersRepository = teachersRepository;   
        _logger = logger;
        _tracer = tracer;
    }

    public async Task<UseCaseResponse<GetTeacherResponse>> Execute(GetTeacherRequest request)
    {
        _logger.LogInformation("Entrou para recuperar os dados da professora", JsonSerializer.Serialize(request));
        using var activity = _tracer.StartActiveSpan("GetTeacherUseCase");
        
        activity.SetAttribute("Request", request.Id.ToString());
        activity.SetAttribute("Solicitante", "Wander V Bergami");


        _logger.LogInformation("User login attempted: Username {Username}, Password {Password}", "eS",1313213);
        _logger.LogWarning("User login failed: Username {Username}", "wANDER.BERGAMI");

        var teachers = await _teachersRepository.Get(request.Id);

        if (teachers == null)
        {
            string message = string.Format(Message.NotFound, request.Id);
            
            return new UseCaseResponse<GetTeacherResponse>().SetNotFound(message, new List<ErrorMessage> { new ErrorMessage(CodeError.NotFound, message) });
        }

        _logger.LogInformation("Obteve o registro", request);


        return new UseCaseResponse<GetTeacherResponse>().SetSuccess(teachers);
        
    }
}


