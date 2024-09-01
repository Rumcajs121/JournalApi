using Journal.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JournalApi.Tests;

public class JournalApiDbContextConnectedTest:IClassFixture<JournalApiDbContextFixture>
{
    private readonly JournalDbContext _dbContext;

    public JournalApiDbContextConnectedTest(JournalApiDbContextFixture fixture)
    {
        _dbContext = fixture.DbContext;
    }
    [Fact]
    public void AddInfrastructure_CanConnectedDb_IsConnected()
    {
        var connect=_dbContext.Database.CanConnect();
        Assert.True(connect, "Db is not connect");
    }
}
public class JournalApiDbContextFixture : IDisposable
{
    public JournalDbContext DbContext { get; private set; }

    public JournalApiDbContextFixture()
    {
        var options = new DbContextOptionsBuilder<JournalDbContext>()
            .UseSqlServer("Connectionstring:JournalApiDb")
            .Options;

        DbContext = new JournalDbContext(options);
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}