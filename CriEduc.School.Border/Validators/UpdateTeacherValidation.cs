using CriEduc.School.Border.Dtos.Teacher;
using FluentValidation;

namespace CriEduc.School.Border.Validators
{
    public class UpdateTeacherValidation : AbstractValidator<UpdateTeacherRequest>
    {
        public UpdateTeacherValidation()
        {
            RuleFor(x => x.Id).NotEmpty();

            Include(new TeacherRequestValidation());
        }
    }
}
