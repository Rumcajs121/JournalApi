using Journal.Application.Commons.Commands.CreateJournal;
using Journal.Application.Commons.Queries.GetAllJournal;
using Journal.Application.Dtos;

namespace Journal.Application;

public interface IJournalRepository
{
    Task<string> CreateJournal(CreateJournalDto dto);
    Task<List<JournalMainDto>> GetAll();
    Task<bool> EditJournal(string id, EditAdctionFormDto dto);
    Task<JournalGetOneDto> GetJournalById(string id);
}