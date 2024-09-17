using FluentValidation;

namespace Journal.Application.Commons.Commands.CreateJournal;

public class CreateJournalValidator:AbstractValidator<CreateJournalCommand.JournalDto>
{
    public CreateJournalValidator()
    {
        RuleFor(c => c.Text)
            .NotEmpty()
            .WithMessage("This section is not empty")
            .MaximumLength(255)
            .WithMessage("This section is to long");
    }
}