using Journal.Application.Dtos;
using MediatR;

namespace Journal.Application.Commons.Commands.EditJournal;

public class EditJournalCommand:IRequest<bool>
{
    public string Id { get; set; }
    public EditAdctionFormDto EditProperty { get; set; }
}