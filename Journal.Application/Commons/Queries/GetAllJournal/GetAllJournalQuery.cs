using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Queries.GetAllJournal;

public class GetAllJournalQuery:IRequest<List<JournalMainDto>>
{
    public JournalMainDto JournalDto { get; set; }
}