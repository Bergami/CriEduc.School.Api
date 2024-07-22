using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;

namespace CriEduc.School.Border.UseCases;
public interface ISearchTeacherUseCase : IUseCase<SearchTeacherRequest, IEnumerable<GetTeacherResponse>>
{
}
