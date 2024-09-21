using MediatR;

namespace Journal.Application.Commons.Commands.EditJournal;

public class EditJournalCommandHandler:IRequestHandler<EditJournalCommand,bool>
{
    private readonly IJournalRepository _repository;

    public EditJournalCommandHandler(IJournalRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(EditJournalCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.EditJournal(request.Id, request.EditProperty);
        return true;
    }
}