

namespace Journal.Domain.Entities;

public class Journal:UniqueId
{
    public string NormalizedId { get; set; } =Guid.NewGuid().ToString();
    public required string ShortDescription { get; set; }
    public string? Text { get; set; }
    public virtual IEnumerable<Picture> Pictures { get; set; } = default!;
    public required int  AuthorId { get; set; }
    public Author Author { get; set; }
}