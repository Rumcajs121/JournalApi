using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Queries.GetAllJournal;

public class GetAllJournalQueryHandler:IRequestHandler<GetAllJournalQuery,List<JournalMainDto>>
{
    public Task<List<JournalMainDto>> Handle(GetAllJournalQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
