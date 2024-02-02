using System.Net;
using System.Net.Mail;
using Contracts.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Service;

public class EmailService(IConfiguration configuration) : IEmailService
{
	public async Task<bool> SendEmail(EmailRequestDto emailRequest, string subject, string body)
	{
		try
		{
			var smtpClient = new SmtpClient(configuration["Mail:Client"] ?? "smtp.gmail.com")
			{
				Port = 587,
				Credentials =
					new NetworkCredential("perisicnikola37@gmail.com", configuration["Mail:Secret"] ?? "secret"),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(configuration["Mail:EmailSender"] ?? "example@gmail.com"),
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};

			mailMessage.To.Add(emailRequest.ToEmail);

			await smtpClient.SendMailAsync(mailMessage);

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);

			throw new EmailException("EmailService.cs");
			throw;
		}
	}

	public async Task<bool> SendEmailWithAttachment(EmailRequestDto emailRequest, string subject, string body, string attachmentFileName, byte[] attachmentContent, string attachmentContentType)
	{
		try
		{
			var smtpClient = new SmtpClient(configuration["Mail:Client"] ?? "smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("perisicnikola37@gmail.com", configuration["Mail:Secret"] ?? "cqzy dvds ngoo pdqm"),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(configuration["Mail:EmailSender"] ?? "example@gmail.com"),
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};

			mailMessage.To.Add(emailRequest.ToEmail);

			// Attach the PDF file
			using (var stream = new MemoryStream(attachmentContent))
			{
				mailMessage.Attachments.Add(new Attachment(stream, attachmentFileName, attachmentContentType));
				await smtpClient.SendMailAsync(mailMessage);
			}

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);

			throw new EmailException("EmailService.cs");
			throw;
		}
	}

}