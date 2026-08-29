using FluentValidation;
using TaskNine.DTOs;

namespace TaskNine.Validators
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.")
                .Must(title => !title.Contains('<')).WithMessage("Title must not contain HTML tags.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("DueDate is required.")
                .GreaterThan(DateTime.Now)
                .When(x => x.DueDate != default)
                .WithMessage("Due date must be in the future.");

            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
