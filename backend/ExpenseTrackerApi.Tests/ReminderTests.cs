using ExpenseTrackerApi.Tests.Fixtures;
using Persistence;

namespace ExpenseTrackerApi.Tests;

public class ReminderControllerTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
	[Fact]
	public async Task GetRemindersAsync_ReturnsSuccessStatusCode()
	{
		// Arrange
		var context = fixture.Context;

		var reminderRepository = new ReminderRepository(context, null!);

		var reminderService = new ReminderService(null!, null!, null!, reminderRepository, null!);
		var controller = new ReminderController(reminderService);

		// Act
		var result = await controller.GetRemindersAsync();

		// Assert
		Assert.IsType<OkObjectResult>(result.Result);
	}
}