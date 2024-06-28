using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Shared;

namespace CriEduc.School.Border.UseCases
{
    public interface IUpdateTeacherUseCase : IUseCase<(UpdateTheacherRequest, Guid), UpdateTheacherResponse>
    {
    }
}
