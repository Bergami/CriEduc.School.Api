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
    public class CreateTeacherUseCase : UseCaseBase<CreateTeacherRequest, CreateTeacherResponse>, ICreateTeacherUseCase
    {
        private readonly ITeachersRepository _teachersRepository;

        public CreateTeacherUseCase(ITeachersRepository teachersRepository,
            ILogger<CreateTeacherUseCase> logger,
            IValidator<CreateTeacherRequest> validator,
            Tracer tracer)
            : base(logger, validator, tracer)
        {
            _teachersRepository = teachersRepository;
        }

        protected override async Task<UseCaseResponse<CreateTeacherResponse>> ExecuteUseCaseAsync(CreateTeacherRequest request)
        {
            var result = await _teachersRepository.Save(request);

            return new UseCaseResponse<CreateTeacherResponse>().SetSuccess(result);
        }
    }
}
