using CriEduc.School.Border.Dtos.Teacher;
using FluentValidation;

namespace CriEduc.School.Border.Validators
{
    public class TeacherRequestValidation : AbstractValidator<TeacherRequest>
    {
        public TeacherRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.WorkPeriod).NotEmpty();
            RuleFor(x => x.Birth).NotEmpty();
            RuleFor(x => x.Workload).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Specialty).NotEmpty();
        }
    }
}
