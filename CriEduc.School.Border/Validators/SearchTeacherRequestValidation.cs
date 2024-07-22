using CriEduc.School.Border.Dtos.Teacher;
using FluentValidation;

namespace CriEduc.School.Border.Validators
{
    public class SearchTeacherRequestValidation : AbstractValidator<SearchTeacherRequest>
    {
        public SearchTeacherRequestValidation()
        {
            RuleFor(x => x.PagingParam)
                .NotEmpty()
                .Must(x=> x.Take <= 100);                
        }
    }   
}
