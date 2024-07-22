using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace CriEduc.School.UseCase.Teacher
{
    public class DeleteTeacherUseCase : UseCaseBase<Guid, DeleteTeacherResponse>, IDeleteTeacherUseCase
    {
        private readonly ITeachersRepository _teachersRepository;
        
        public DeleteTeacherUseCase(ITeachersRepository teachersRepository,
            ILogger<DeleteTeacherUseCase> logger,
            IValidator<Guid> validator, Tracer tracer) : base(logger, validator, tracer)
        {
            _teachersRepository = teachersRepository;           
        }

        protected override async Task<UseCaseResponse<DeleteTeacherResponse>> ExecuteUseCaseAsync(Guid id)        
        {
            if (await _teachersRepository.Get(id) is null)
            {
                _logger.LogInformation(Message.NotFoundErrorTeacher(id));

                var error = new ErrorMessage(CodeError.NotFound, Message.NotFoundErrorTeacher(id));

                return new UseCaseResponse<DeleteTeacherResponse>().SetNotFound(nameof(DeleteTeacherResponse), new[] { error });
            }

            await _teachersRepository.Delete(id);

            return new UseCaseResponse<DeleteTeacherResponse>().SetSuccessNoContent();                       
        }
    }
}
