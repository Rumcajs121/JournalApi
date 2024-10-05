using Journal.Application;
using Journal.Application.Commons.Commands.CreateJournal;
using Journal.Application.Commons.Queries.GetAllJournal;
using Journal.Application.Dtos;
using Journal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Repository;

public class JournalRepository:IJournalRepository
{
    private readonly JournalDbContext _dbContext;

    public JournalRepository(JournalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateJournal(CreateJournalDto dto)
    {
        var newJournal = new Domain.Entities.Journal
        {
            ShortDescription = dto.ShortDescription,
            Text = dto.Text,
            AuthorId = dto.AuthorId,
        };
        _dbContext.Journals.Add(newJournal);
        await _dbContext.SaveChangesAsync();
        return newJournal.NormalizedId;
    }
    
    
    public async Task<List<JournalMainDto>> GetAll()
    {
        var journalAll = await _dbContext.Journals
            .Include(x => x.Author)
            .Include(journal => journal.Pictures)
            .Select(journal => new JournalMainDto(
                journal.NormalizedId,
                journal.ShortDescription,
                journal.Text,
                journal.Pictures
                    .Select(p => "https://journalapisane.blob.core.windows.net/pictures/" + p.GuidNormalizedName)
                    .ToList(),
                journal.Author.Nick,
                journal.Author.ImgAvatar,
                journal.Author.SumJournal,
                journal.Author.CreateAccount,
                journal.Author.LastLogin,
                journal.Author.TheBestJournal))
            .ToListAsync();
    
        return journalAll;
    }

    public async Task<bool> EditJournal(string id, EditAdctionFormDto dto)
    {
        var  jorunal = await _dbContext.Journals
            .FirstOrDefaultAsync(x => x.NormalizedId == id);
        if ( jorunal == null)
        {
            return false; 
        }
        jorunal.ShortDescription = dto.ShortDescription;
        jorunal.Text = dto.Text;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async  Task<JournalGetOneDto> GetJournalById(string id)
    {
        var journalEntity = await _dbContext.Journals
            .Include(x => x.Author)
            .Include(journal => journal.Pictures)
            .FirstOrDefaultAsync(x => x.NormalizedId == id);
        if (journalEntity is null)
        {
            throw new InvalidOperationException("Journal is not found");
        }

        var journalDto = new JournalGetOneDto(
            journalEntity.NormalizedId,
            journalEntity.ShortDescription,
            journalEntity.Text,
            journalEntity.Pictures
                .Select(p => "https://journalapisane.blob.core.windows.net/pictures/" + p.GuidNormalizedName)
                .ToList(),
            journalEntity.Author.Nick,
            journalEntity.Author.ImgAvatar
            );
        return journalDto;

    }
}