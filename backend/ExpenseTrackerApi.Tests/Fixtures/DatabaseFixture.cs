namespace ExpenseTrackerApi.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseContext Context { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new DatabaseContext(options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
