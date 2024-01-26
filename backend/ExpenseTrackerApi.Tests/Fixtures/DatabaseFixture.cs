namespace ExpenseTrackerApi.Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        Context = new DatabaseContext(options);
    }

    public DatabaseContext Context { get; }

    public void Dispose()
    {
        Context.Dispose();
    }
}