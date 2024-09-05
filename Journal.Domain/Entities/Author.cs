namespace Journal.Domain.Entities;

public class Author:UniqueId
{
    public required string Nick { get; set; }
    public string? ImgAvatar { get; set; }
    public int SumJournal { get; set; }
    public DateTime CreateAccount { get; set; }
    public DateTime LastLogin { get; set; }
    public string? TheBestJournal { get; set; }
    public virtual ICollection<Journal> Journals { get; set; }
    
}