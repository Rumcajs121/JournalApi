using Journal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure;

public class JournalDbContext:DbContext
{
    public JournalDbContext(DbContextOptions<JournalDbContext> options):base(options)
    {
        
    }
    public DbSet<Domain.Entities.Journal>Journals { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Journal>(eb =>
        {
            eb.HasMany(w => w.Pictures)
                .WithOne(c => c.Journals)
                .HasForeignKey(c => c.JournalId);
        });
    }
}