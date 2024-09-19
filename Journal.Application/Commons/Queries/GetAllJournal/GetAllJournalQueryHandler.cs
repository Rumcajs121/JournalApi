using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Queries.GetAllJournal;

public class GetAllJournalQueryHandler:IRequestHandler<GetAllJournalQuery,List<JournalMainDto>>
{
    private readonly IJournalRepository _repository;

    public GetAllJournalQueryHandler(IJournalRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<JournalMainDto>> Handle(GetAllJournalQuery request, CancellationToken cancellationToken)
    {
        var result=await _repository.GetAll();
        return result;
    }
}
