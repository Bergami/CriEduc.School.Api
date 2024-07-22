using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Border.Validators;
using CriEduc.School.Repository.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System.ComponentModel.DataAnnotations;

namespace CriEduc.School.UseCase.Teacher
{
    public class SearchTeacherUseCase : UseCaseBase<SearchTeacherRequest, IEnumerable<GetTeacherResponse>> ,ISearchTeacherUseCase
    {
        private readonly ITeachersRepository _teachersRepository;
       
        public SearchTeacherUseCase(ITeachersRepository teachersRepository,
                                ILogger<SearchTeacherUseCase> logger,
                                IValidator<SearchTeacherRequest> validator,
                                Tracer tracer): base(logger, validator, tracer)
        {
            _teachersRepository = teachersRepository;       
        }

        protected override async Task<UseCaseResponse<IEnumerable<GetTeacherResponse>>> ExecuteUseCaseAsync(SearchTeacherRequest request)
        {
            var (result, totalCount) = await _teachersRepository.Search(request);

            var response =  new UseCaseResponse<IEnumerable<GetTeacherResponse>>().SetSuccess(result);

            // Adicionando informações aos cabeçalhos
            response.Headers.Add("X-Total-Count", totalCount.ToString());
            response.Headers.Add("X-Data-Count", result.Count().ToString());

            return response;
        }
    }
}
