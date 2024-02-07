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
public class UserController(
    DatabaseContext context,
    IEmailService emailService,
    IValidator<User> validator,
    PdfGenerator pdfGenerator,
    IGetAuthenticatedUserIdService getAuthenticatedUserId)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await context.Users
            .Include(e => e.Expenses)
            .Include(e => e.Incomes)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        return users;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }

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
            throw new ConflictException();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        var validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

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
                .Include(u => u.Incomes)!
                .ThenInclude(i => i.IncomeGroup)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return BadRequest("User not found");

            var incomesHtml = string.Join("<br />",
                user.Incomes!.Select(income =>
                {
                    return
                        $"<div class='flex items-center justify-between py-2 border-b border-gray-300'><span class='text-gray-700'>Description: {income.Description} <span class=`ml-10`>Group: {income.IncomeGroup?.Name}</span></span><span class='text-green-500'>&nbsp; +${income.Amount}</span></div>{income.CreatedAt:yyyy-MM-dd HH:mm:ss}";
                }));

            var statsId = GenerateStatsId();
            var statsDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var emailBody =
                $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
			<html dir=""ltr"" lang=""en"">
			<head>
				<meta content=""text/html; charset=UTF-8"" http-equiv=""Content-Type"" />
				<link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css"" rel=""stylesheet"">
			</head>
			<body style=""background-color:#fff;font-family:'Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'Segoe UI', Roboto, 'Helvetica Neue', Arial, 'Noto Sans', sans-serif"">
				<div class='max-w-2xl mx-auto p-4'>
					<img src='https://i.postimg.cc/VsKQJpRb/logo.png' alt='Linear Logo' class='block mx-auto mb-4' style='width: 42px; height: 42px;'>
					<h1 class='text-2xl font-semibold text-gray-800 mb-6'>Expense Tracker</h1>
					<hr class='border-t border-gray-300 my-8'>
					<p class='text-gray-700 text-sm mb-4'>Incomes:</p>
					{incomesHtml}
					<div class=""mt-10"">
					<p class='text-gray-700 text-sm mb-4'>Stats ID: {statsId}</p>
					<p class='text-gray-700 text-sm mb-4'>Stats creation date: {statsDate}</p>
					<p class='text-gray-700 text-sm'>Thank you for using Expense Tracker&trade;</p>
					</div>
				</div>
			</body>
			</html>";

            var pdfBytes = pdfGenerator.GeneratePdf(emailBody);

            var isEmailSent = await emailService.SendEmailWithAttachment(emailRequest, "Expense Tracker", emailBody,
                "expense_tracker.pdf", pdfBytes, "application/pdf");

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

    [HttpDelete("delete/account")]
    public async Task<IActionResult> DeleteAuthenticatedUser()
    {
        try
        {
            var userId = getAuthenticatedUserId.GetUserId(User);

            var user = await context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static string GenerateStatsId()
    {
        return Guid.NewGuid().ToString("N")[..8].ToUpper();
    }

    private bool UserExists(int id)
    {
        return context.Users.Any(e => e.Id == id);
    }
}