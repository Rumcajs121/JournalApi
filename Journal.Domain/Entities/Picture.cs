namespace Journal.Domain.Entities;

public class Picture:UniqueId
{
    public string? GuidNormalizedName{ get; set; }
    public string?  TypeFile { get; set; }
    public int JournalId { get; set; }
    public Journal Journal { get; set; }
}