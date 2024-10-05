using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Queries.GetOneJournalById;

public class GetOneJournalQueryHandler:IRequestHandler<GetOneJournalQuery,JournalGetOneDto>
{
    private readonly IJournalRepository _service;

    public GetOneJournalQueryHandler(IJournalRepository service)
    {
        _service = service;
    }
    public Task<JournalGetOneDto> Handle(GetOneJournalQuery request, CancellationToken cancellationToken)
    {
        return _service.GetJournalById(request.NormalizedGuid);
    }
}