using MediatR;
using Microsoft.Extensions.Azure;

namespace Journal.Application.Commons.Commands.CreateJournal;

public class CreateJournalCommandHandler:IRequestHandler<CreateJournalCommand,string>
{
    private readonly IJournalRepository _repository;

    public CreateJournalCommandHandler(IJournalRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> Handle(CreateJournalCommand request, CancellationToken cancellationToken)
    {
        var result=await _repository.CreateJournal(request.Dto);
        return result;
    }
}