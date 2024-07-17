using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CriEduc.School.UseCase.Teacher
{
    public class CreateTeacherUseCase : ICreateTeacherUseCase
    {        
        private readonly ITeachersRepository _teachersRepository;
        private readonly IValidator<CreateTeacherRequest> _validator;
        private readonly ILogger<CreateTeacherUseCase> logger;

        public CreateTeacherUseCase(ITeachersRepository teachersRepository, IValidator<CreateTeacherRequest> validator)
        {
            _teachersRepository = teachersRepository;
            _validator = validator;           
        }

        public async Task<UseCaseResponse<CreateTeacherResponse>> Execute(CreateTeacherRequest request)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                var result = await _teachersRepository.Save(request);

                return new UseCaseResponse<CreateTeacherResponse>().SetSuccess(result);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x=> new ErrorMessage(x.ErrorCode, x.ErrorMessage));

                return new UseCaseResponse<CreateTeacherResponse>().SetBadResquest(Message.ValidateRequest, errors);
            }            
        }       
    }
}
