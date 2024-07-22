using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System.Text.Json;

namespace CriEduc.School.UseCase.Teacher;

public class GetTeacherUseCase : UseCaseBase<Guid, GetTeacherResponse>, IGetTeacherUseCase
{    
    private readonly ITeachersRepository _teachersRepository;
    
    public GetTeacherUseCase(ITeachersRepository teachersRepository, 
                            ILogger<CreateTeacherUseCase> logger,
                            IValidator<Guid> validator,
                            Tracer tracer) : base(logger, validator, tracer)
    {       
        _teachersRepository = teachersRepository;          
    }

    protected override async Task<UseCaseResponse<GetTeacherResponse>> ExecuteUseCaseAsync(Guid id)
    {      
        var teachers = await _teachersRepository.Get(id);

        if (teachers == null)
        {
            var error = new ErrorMessage(CodeError.NotFound, Message.NotFoundErrorTeacher(id)); 

            return new UseCaseResponse<GetTeacherResponse>().SetNotFound(nameof(GetTeacherUseCase), new List<ErrorMessage> { error });
        }

        return new UseCaseResponse<GetTeacherResponse>().SetSuccess(teachers);        
    }
}


