using FluentValidation;
using Journal.Application.Dtos;

namespace Journal.Application.Commons.Commands.CreateJournal;

public class CreateJournalValidator:AbstractValidator<CreateJournalCommand>
{
    public CreateJournalValidator()
    {
        RuleFor(c => c.Dto.Text)
            .NotEmpty()
            .WithMessage("This section is not empty")
            .MinimumLength(3)
            .WithMessage("Text must be at least 3 characters")
            .MaximumLength(255)
            .WithMessage("Text must not exceed 255 characters");
        RuleFor(c => c.Dto.ShortDescription)
            .NotEmpty()
            .WithMessage("This section is not empty")
            .MinimumLength(3)
            .WithMessage("Short description must be at least 3 characters")
            .MaximumLength(100)
            .WithMessage("Short description must not exceed 100 characters");
    }
}