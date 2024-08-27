using System.Net;

namespace Journal.Domain.Entities;

public class Journal:UniqueId
{
    public string NormalizedId { get; set; } =Guid.NewGuid().ToString();
    public string ShortDescription { get; set; }
    public string Text { get; set; }
    public virtual IEnumerable<Picture> Pictures { get; set; }
    public Author  Author { get; set; }
}