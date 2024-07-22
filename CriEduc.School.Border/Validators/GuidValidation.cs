using FluentValidation;

namespace CriEduc.School.Border.Validators
{
    public class GuidValidation : AbstractValidator<Guid>
    {
        public GuidValidation()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Id is not valid.");
        }
    }
}
