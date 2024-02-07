using Contracts.Dto;
using MailChimp.Net;
using MailChimp.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;

namespace Presentation.Controllers;

[ApiController]
[Route("api/mailchimp")]
[EnableRateLimiting("fixed")]
public class MailchimpController : ControllerBase
{
	private readonly string _audienceId = "5fe973b75d";
	private readonly MailChimpManager _mailChimpManager;

	public MailchimpController(IConfiguration configuration)
	{
		var apiKey = configuration["MailChimp:ApiKey"] ??
					 throw new ApplicationException("MailChimp API key is not configured.");
		_audienceId = configuration["MailChimp:AudienceId"] ??
					  throw new ApplicationException("MailChimp AudienceId is not configured.");

		_mailChimpManager = new MailChimpManager(apiKey);
	}

	[HttpPost("subscribe")]
	public async Task<IActionResult> Subscribe([FromBody] SubscriberRequest model)
	{
		try
		{
			var member = new Member { EmailAddress = model.Email, Status = Status.Subscribed };
			await _mailChimpManager.Members.AddOrUpdateAsync(_audienceId, member);

			return Ok("Subscriber added successfully");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex}");

			return BadRequest($"Failed to add subscriber: {ex.Message}");
		}
	}

	[HttpGet("members")]
	public IActionResult GetMembers()
	{
		try
		{
			var members = _mailChimpManager.Members.GetAllAsync(_audienceId).Result;

			return Ok(members);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex}");

			return BadRequest($"Failed to retrieve members: {ex.Message}");
		}
	}
}