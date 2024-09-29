using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Commands.CreateJournal;

public class CreateJournalCommand : IRequest<string>
{
    public CreateJournalDto Dto { get; set; }

    public CreateJournalCommand(CreateJournalDto dto)
    {
        Dto = dto;
    }
}