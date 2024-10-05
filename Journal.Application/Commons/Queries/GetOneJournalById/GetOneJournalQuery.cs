using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Queries.GetOneJournalById;

public class GetOneJournalQuery : IRequest<JournalGetOneDto>
{
    public string NormalizedGuid { get; set; }
}
