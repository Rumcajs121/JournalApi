using FluentValidation;
using Journal.Application.Dtos;

namespace Journal.Application.Commons.Commands.EditJournal;

public class EditJournalValidator:AbstractValidator<EditAdctionFormDto>
{
    public EditJournalValidator()
    {
        RuleFor(c=>c.Text)
            .NotEmpty()
            .WithMessage("This section is not empty")
            .MinimumLength(3)
            .WithMessage("This section is not empty")
            .MaximumLength(255)
            .WithMessage("This section is to long");
        RuleFor(c=>c.ShortDescription)
            .NotEmpty()
            .WithMessage("This section is not empty")
            .MinimumLength(3)
            .WithMessage("This section is not empty")
            .MaximumLength(100)
            .WithMessage("This section is to long");
    }
    
}