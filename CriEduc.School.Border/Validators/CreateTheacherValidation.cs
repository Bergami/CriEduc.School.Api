using CriEduc.School.Border.Dtos.Teacher;
using FluentValidation;

namespace CriEduc.School.Border.Validators
{
    public class CreateTheacherValidation : AbstractValidator<CreateTeacherRequest>
    {
        public CreateTheacherValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.WorkPeriod).NotEmpty();
            RuleFor(x => x.Birth).NotEmpty();
            RuleFor(x => x.Workload).NotEmpty();
            RuleFor(x => x.Specialty).NotEmpty();
        }
    }
}
