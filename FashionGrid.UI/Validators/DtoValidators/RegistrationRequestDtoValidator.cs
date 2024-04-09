using FashionGrid.UI.Models.Dtos;
using FluentValidation;

namespace FashionGrid.UI.Validators.DtoValidators
{
    public class RegistrationRequestDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address format");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("Name must contain only letters and spaces");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[0-9]*$").WithMessage("Phone number must contain only digits")
                .Length(10).WithMessage("Phone number must be 10 digits");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");

        }
    }
}
