using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace CriEduc.School.UseCase.Teacher
{
    public class SearchTeacherUseCase : ISearchTeacherUseCase
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly Tracer _tracer;

        private readonly ILogger<SearchTeacherUseCase> _logger;
        public SearchTeacherUseCase(ITeachersRepository teachersRepository,
                                ILogger<SearchTeacherUseCase> logger,
                                Tracer tracer)
        {
            _teachersRepository = teachersRepository;
            _logger = logger;
            _tracer = tracer;
        }

        public async Task<UseCaseResponse<IEnumerable<GetTeacherResponse>>> Execute(SearchTeacherRequest request)
        {
            var result = await _teachersRepository.Search(request);

            return new UseCaseResponse<IEnumerable<GetTeacherResponse>>().SetSuccess(result);
        }
    }
}
