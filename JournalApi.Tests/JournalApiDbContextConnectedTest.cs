using Journal.Infrastructure;

namespace JournalApi.Tests;

public class JournalApiDbContextConnectedTest
{
    private readonly JournalDbContext _dbContext;

    public JournalApiDbContextConnectedTest(JournalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [Fact]
    public void AddInfrastructure_CanConnectedDb_IsConnected()
    {
        var connect=_dbContext.Database.CanConnect();
        Assert.True(connect, "Db is not connect");
    }
}