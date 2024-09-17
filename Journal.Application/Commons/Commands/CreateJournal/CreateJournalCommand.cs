using MediatR;

namespace Journal.Application.Commons.Commands.CreateJournal;

public class CreateJournalCommand:IRequest<string>
{
    public JournalDto Dto { get; }

    public CreateJournalCommand(JournalDto dto)
    {
        Dto = dto;
    }
    public record JournalDto(string ShortDescription, string Text, int AuthorId);
}