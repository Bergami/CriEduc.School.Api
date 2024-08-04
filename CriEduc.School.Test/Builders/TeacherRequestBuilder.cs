using Bogus;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.Enum;

namespace CriEduc.School.Test.Builders
{
    public abstract class TeacherRequestBuilder
    {
        protected TeacherRequest _teacherRequest;
        protected TeacherRequestBuilder()
        {
            _teacherRequest = TeacherRequestBuild();
        }
        private TeacherRequest TeacherRequestBuild()
        {
            return new Faker<CreateTeacherRequest>("pt_BR")
                    .RuleFor(x => x.WorkPeriod, f => f.PickRandom<Period>())
                    .RuleFor(x => x.Workload, f => f.Random.Int(min: 10, max: 50))
                    .RuleFor(x => x.Birth, f => f.Person.DateOfBirth)
                    .RuleFor(x => x.Specialty, f => f.Name.JobTitle())
                    .RuleFor(x => x.Name, f => f.Person.FullName);
        }
    }
}
