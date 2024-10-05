using Azure;
using Azure.Data.Tables;

namespace Journal.Application.Dtos;

public class TestTableInAzure:ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string FullNamw { get; set; }
    public DateTime DateOfBirth { get; set; }
    
}