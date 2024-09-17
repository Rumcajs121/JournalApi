using Journal.Application;
using Journal.Application.Commons.Commands.CreateJournal;
using Journal.Application.Commons.Queries.GetAllJournal;
using Journal.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Repository;

public class JournalRepository:IJournalRepository
{
    private readonly JournalDbContext _dbContext;

    public JournalRepository(JournalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateJournal(CreateJournalCommand.JournalDto dto)
    {
        var newJournal = new Domain.Entities.Journal
        {
            ShortDescription = dto.ShortDescription,
            Text = dto.Text,
            AuthorId = dto.AuthorId,
        };
        _dbContext.Journals.Add(newJournal);
        // await _dbContext.SaveChangesAsync();
        return newJournal.NormalizedId;
    }
    
    
    public List<JournalMainDto> GetAll()
    {
        List<JournalMainDto> response = [];
        var journalAll = _dbContext.Journals
            .Include(x => x.Author)
            .Include(journal => journal.Pictures)
            .Select(journal => new JournalMainDto
            {
                NormalizedId = journal.NormalizedId,
                ShortDescription = journal.ShortDescription,
                Text = journal.Text,
                Pictures = journal.Pictures.Select(p => new PictureDto
                {
                    GuidNormalizedName = p.GuidNormalizedName,
                    UriBlobStorage = "https://journalapisane.blob.core.windows.net/pictures/"
                }).ToList(),  // Projektowanie obrazów na listę PictureDto
                Nick = journal.Author.Nick,
                ImgAvatar = journal.Author.ImgAvatar,
                SumJournal = journal.Author.SumJournal,
                CreateAccount = journal.Author.CreateAccount,
                LastLogin = journal.Author.LastLogin,
                TheBestJournal = journal.Author.TheBestJournal
            })
            .ToList();
        return response;
    }
}