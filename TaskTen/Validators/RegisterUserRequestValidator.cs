using FluentValidation;
using TaskTen.DTOs;

namespace TaskTen.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequestDTO>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.")
                .Must(title => !title.Contains('<')).WithMessage("Name must not contain HTML tags.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.")
                .Must(title => !title.Contains('<')).WithMessage("Email must not contain HTML tags.")
                .EmailAddress().WithMessage("Email must be valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password cannot be less than 8 characters.")
                .MaximumLength(200).WithMessage("Password cannot exceed 200 characters.");
        }
    }
}
