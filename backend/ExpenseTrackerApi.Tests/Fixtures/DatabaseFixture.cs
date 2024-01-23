namespace ExpenseTrackerApi.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public MainDatabaseContext Context { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new MainDatabaseContext(options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
