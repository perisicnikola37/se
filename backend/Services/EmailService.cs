using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

public class EmailService
{
	private readonly IConfiguration _configuration;

	public EmailService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task<bool> SendEmail(EmailRequest emailRequest, string subject, string body)
	{
		try
		{
			var smtpClient = new SmtpClient(_configuration["Mail:Client"])
			{
				Port = 587,
				Credentials = new NetworkCredential("perisicnikola37@gmail.com", _configuration["Mail:Secret"]),
				EnableSsl = true,
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(_configuration["Mail:EmailSender"]),
				Subject = subject,
				Body = body,
				IsBodyHtml = true,
			};

			mailMessage.To.Add(emailRequest.ToEmail);

			await smtpClient.SendMailAsync(mailMessage);

			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}
}


