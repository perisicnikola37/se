using Contracts.Dto;
using Domain.Exceptions;
using Domain.Models;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Presentation.Controllers;

[Route("api/users")]
[ApiController]
[EnableRateLimiting("fixed")]
public class UserController(DatabaseContext context, EmailService emailservice, IValidator <User> validator) : ControllerBase
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
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutUser(int id, User user)
	{
		if (id != user.Id) return BadRequest();

		context.Entry(user).State = EntityState.Modified;

		try
		{
			await context.SaveChangesAsync();
		}
		catch (ConflictException)
		{
			if (!UserExists(id))
				return NotFound();
			throw;
		}

		return NoContent();
	}

	// POST: api/User
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<User>> PostUser(User user)
	{
		var validationResult = await validator.ValidateAsync(user);
		if (!validationResult.IsValid)
		{
			return BadRequest(validationResult.Errors);
		}
		
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

	// POST: api/User/SendEmail
	[HttpPost("email/send")]
	public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
	{
		try
		{
			var isEmailSent = await emailservice.SendEmail(emailRequest, "subject", "body");

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