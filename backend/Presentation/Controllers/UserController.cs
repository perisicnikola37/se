using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/users")]
[ApiController]
[EnableRateLimiting("fixed")]
public class UserController(DatabaseContext context, IEmailService emailService, IValidator<User> validator, PdfGenerator pdfGenerator, IGetAuthenticatedUserIdService getAuthenticatedUserId)
	: ControllerBase
{
	// GET: api/User
	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetUsers()
	{
		var users = await context.Users
			.Include(e => e.Expenses)
			.Include(e => e.Incomes)
			.OrderByDescending(e => e.CreatedAt)
			.ToListAsync();

		if (users.Count != 0)
			return users;
		return Ok(users);
	}

	// GET: api/User/5
	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser(int id)
	{
		var user = await context.Users.FindAsync(id);

		if (user == null) return NotFound();

		return user;
	}

	// PUT: api/User/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutUser(int id, User user)
	{
		if (id != user.Id) return BadRequest();

		context.Entry(user).State = EntityState.Modified;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (Exception)
		{
			if (!UserExists(id))
				return NotFound();
			throw new ConflictException("UserController.cs");
		}

		return NoContent();
	}

	// POST: api/User
	[HttpPost]
	public async Task<ActionResult<User>> PostUser(User user)
	{
		var validationResult = await validator.ValidateAsync(user);
		if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

		context.Users.Add(user);
		await context.SaveChangesAsync();

		return CreatedAtAction("GetUser", new { id = user.Id }, user);
	}

	// DELETE: api/User/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(int id)
	{
		var user = await context.Users.FindAsync(id);
		if (user == null) return NotFound();

		context.Users.Remove(user);
		await context.SaveChangesAsync();

		return NoContent();
	}


	[HttpPost("email/send")]
	public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto emailRequest)
	{
		try
		{
			var userId = getAuthenticatedUserId.GetUserId(User);

			var user = await context.Users
				.Include(u => u.Incomes)
				.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
			{
				return BadRequest("User not found");
			}

			// Format the incomes for inclusion in the email body
			string incomesHtml = string.Join("<br />", user.Incomes.Select(income =>
			{
				return $"<div class='flex items-center justify-between py-2 border-b border-gray-300'><span class='text-gray-700'>{income.Description}</span><span class='text-green-500'>&nbsp; {income.Amount}</span></div>";
			}));

			string emailBody = $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
		<html dir=""ltr"" lang=""en"">
		  <head>
			<meta content=""text/html; charset=UTF-8"" http-equiv=""Content-Type"" />
		<script src=""https://cdn.tailwindcss.com""></script>
		  </head>
		  <div style=""display:none;overflow:hidden;line-height:1px;opacity:0;max-height:0;max-width:0"">Your login code for Linear<div></div>
		  </div>
		
		  <body style=""background-color:#ffffff;font-family:'Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'Segoe UI', Roboto, 'Helvetica Neue', Arial, 'Noto Sans', sans-serif"">
			<div class='max-w-2xl mx-auto p-4'>
				<img src='https://react-email-demo-7s5r0trkn-resend.vercel.app/static/linear-logo.png' alt='Linear Logo' class='block mx-auto mb-4' style='width: 42px; height: 42px;'>
				<h1 class='text-2xl font-semibold text-gray-800 mb-6'>Your login code for Linear</h1>
				<div class='bg-blue-600 text-white rounded-md p-4'>
					<a href='https://linear.app' class='block text-center font-semibold text-lg' target='_blank'>Login to Linear</a>
				</div>
				<p class='text-gray-700 text-sm mt-6'>This link and code will only be valid for the next 5 minutes. If the link does not work, you can use the login verification code directly:</p>
				<code class='bg-gray-200 text-gray-800 px-2 py-1 font-semibold text-lg mt-2'>tt226-5398x</code>
				<hr class='border-t border-gray-300 my-8'>
				<p class='text-gray-700 text-sm mb-4'>Incomes:</p>
				{incomesHtml}
				<hr class='border-t border-gray-300 my-8'>
				<p class='text-gray-700 text-sm'>Linear</p>
			</div>
		  </body>
		</html>";

			var isEmailSent = await emailService.SendEmail(emailRequest, "subject", emailBody);

			byte[] pdfBytes = pdfGenerator.GeneratePdf(emailBody);

			await emailService.SendEmailWithAttachment(emailRequest, "subject", emailBody, "your_incomes&expenses.pdf", pdfBytes, "application/pdf");

			if (isEmailSent)
				return Ok("Email sent successfully");

			return BadRequest("Failed to send email");
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	private bool UserExists(int id)
	{
		return context.Users.Any(e => e.Id == id);
	}
}