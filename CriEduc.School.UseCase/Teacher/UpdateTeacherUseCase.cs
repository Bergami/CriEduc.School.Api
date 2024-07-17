using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using CriEduc.School.Border.Shared.Enum;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using CriEduc.School.Border.Constants;
using FluentValidation;
using CriEduc.School.Repository.UoW;

namespace CriEduc.School.UseCase.Teacher
{
    public class UpdateTeacherUseCase : IUpdateTeacherUseCase
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly ILogger<CreateTeacherUseCase> _logger;
        private readonly IValidator<UpdateTeacherRequest> _validator;
      
        public UpdateTeacherUseCase(
            ITeachersRepository teachersRepository,
            ILogger<CreateTeacherUseCase> logger,
            IValidator<UpdateTeacherRequest> validator)
        {
            _teachersRepository = teachersRepository;
            _logger = logger;      
            _validator = validator;
        }
        public async Task<UseCaseResponse<UpdateTheacherResponse>> Execute(UpdateTeacherRequest request)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                if (await _teachersRepository.Get(request.Id) is null)
                {
                    var error = new ErrorMessage(CodeError.NotFound, Message.NotFoundErrorTeacher(request.Id));

                    return new UseCaseResponse<UpdateTheacherResponse>().SetNotFound(nameof(UpdateTeacherUseCase), new[] { error });
                }
             
                return await UpdateTheacher(request);                
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x => new ErrorMessage(x.ErrorCode, x.ErrorMessage));

                return new UseCaseResponse<UpdateTheacherResponse>().SetBadResquest(Message.ValidateRequest, errors);
            }
        }

        private async Task<UseCaseResponse<UpdateTheacherResponse>> UpdateTheacher(UpdateTeacherRequest request)
        {
            if (!await _teachersRepository.UpdateTeacherAsync(request))
                return new UseCaseResponse<UpdateTheacherResponse>().SetStatus(UseCaseResponseKind.InternalServerError, Message.InternalServerErrorTeacherUpdate);

            return new UseCaseResponse<UpdateTheacherResponse>().SetSuccessNoContent();
        }
    }
}
