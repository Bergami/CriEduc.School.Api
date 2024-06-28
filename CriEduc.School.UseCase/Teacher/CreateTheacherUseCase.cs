using CriEduc.School.Border.Constants;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using CriEduc.School.Repository.UoW;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CriEduc.School.UseCase.Teacher
{
    public class CreateTheacherUseCase : ICreateTeacherUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeachersRepository _teachersRepository;
        private readonly IValidator<CreateTeacherRequest> _validator;
        private readonly ILogger<CreateTheacherUseCase> logger;

        public CreateTheacherUseCase(IUnitOfWork unitOfWork, ITeachersRepository teachersRepository, IValidator<CreateTeacherRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _teachersRepository= teachersRepository;
            _validator = validator;           
        }

        public async Task<UseCaseResponse<CreateTeacherResponse>> Execute(CreateTeacherRequest request)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                var result = await Create(request);

                return new UseCaseResponse<CreateTeacherResponse>().SetSuccess(result);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x=> new ErrorMessage(x.ErrorCode, x.ErrorMessage));

                return new UseCaseResponse<CreateTeacherResponse>().SetBadResquest(Message.BadRequest, errors);
            }            
        }

        private async Task<CreateTeacherResponse> Create(CreateTeacherRequest request)
        {
            CreateTeacherResponse response;

            _unitOfWork.BeginTransaction();

            response = await _teachersRepository.Save(request);

            _unitOfWork.Commit();

            return response;
        }
    }
}
