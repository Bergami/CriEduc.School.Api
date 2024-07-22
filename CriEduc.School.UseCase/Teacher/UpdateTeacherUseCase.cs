using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.Shared.Enum;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace CriEduc.School.UseCase.Teacher
{
    public class UpdateTeacherUseCase : UseCaseBase<UpdateTeacherRequest, UpdateTheacherResponse>,IUpdateTeacherUseCase
    {
        private readonly ITeachersRepository _teachersRepository;
             
        public UpdateTeacherUseCase(
            ITeachersRepository teachersRepository,
            ILogger<CreateTeacherUseCase> logger,
            IValidator<UpdateTeacherRequest> validator, 
            Tracer tracer): base(logger, validator, tracer) 
        {
            _teachersRepository = teachersRepository;       
        }
        protected override async Task<UseCaseResponse<UpdateTheacherResponse>> ExecuteUseCaseAsync(UpdateTeacherRequest request)
        {           
         
            if (await _teachersRepository.Get(request.Id) is null)
            {
                _logger.LogInformation(Message.NotFoundErrorTeacher(request.Id), request);

                var error = new ErrorMessage(CodeError.NotFound, Message.NotFoundErrorTeacher(request.Id));

                return new UseCaseResponse<UpdateTheacherResponse>().SetNotFound(nameof(UpdateTeacherUseCase), new[] { error });
            }

            if (!await _teachersRepository.UpdateTeacherAsync(request))
            {
                _logger.LogError(message: Message.InternalServerErrorTeacherUpdate, args: request);

                return new UseCaseResponse<UpdateTheacherResponse>().SetStatus(UseCaseResponseKind.InternalServerError, Message.InternalServerErrorTeacherUpdate);
            }

            return new UseCaseResponse<UpdateTheacherResponse>().SetSuccessNoContent();
        }       
    }
}
