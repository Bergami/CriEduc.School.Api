using CriEduc.School.Border.Dtos.Teacher;
using FluentValidation;

namespace CriEduc.School.Border.Validators
{
    public class CreateTeacherValidation : AbstractValidator<CreateTeacherRequest>
    {
        public CreateTeacherValidation()
        {
            // Chama o validador da classe base (TheacherRequestValidation)
            Include(new TeacherRequestValidation());
        }
    }   
}
