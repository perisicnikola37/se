using Castle.Core.Logging;
using ExpenseTrackerApi.Tests.Fixtures;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseTrackerApi.Tests
{
	public class ReminderControllerTests : IClassFixture<DatabaseFixture>
	{
		private readonly DatabaseFixture _fixture;

		public ReminderControllerTests(DatabaseFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public async Task GetRemindersAsync_ReturnsSuccessStatusCode()
		{
			// Arrange
			var context = _fixture.Context;

			var reminderRepository = new ReminderRepository(context, null!);

			var reminderService = new ReminderService(null!, null!, null!, reminderRepository, null!);
			var controller = new ReminderController(reminderService);

			// Act
			var result = await controller.GetRemindersAsync();

			// Assert
			Assert.IsType<OkObjectResult>(result.Result);
		}

		[Fact]
		public async Task GetReminderAsync_ReturnsNotFoundForNonExistentReminder()
		{
			// Arrange
			var context = _fixture.Context;

			var reminderRepository = new ReminderRepository(context, null!);

			var logger = new LoggerFactory().CreateLogger<ReminderService>();
			var reminderService = new ReminderService(logger, null!, null!, reminderRepository, null!);
			var controller = new ReminderController(reminderService);

			// Act
			var result = await controller.GetReminderAsync(123103);

			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}
	}
}
