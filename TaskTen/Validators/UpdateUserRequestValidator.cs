using FluentValidation;
using TaskTen.DTOs;

namespace TaskTen.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequestDTO>
    {
        public UpdateUserRequestValidator()
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

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current Password is required.")
                .MinimumLength(8).WithMessage("Current Password cannot be less than 8 characters.")
                .MaximumLength(200).WithMessage("Current Password cannot exceed 200 characters.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.")
                .MinimumLength(8).WithMessage("NewPassword cannot be less than 8 characters.")
                .MaximumLength(200).WithMessage("NewPassword cannot exceed 200 characters.");
        }
    }
}
