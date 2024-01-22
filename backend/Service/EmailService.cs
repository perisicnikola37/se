using System.Net;
using System.Net.Mail;
using Contracts.Dto;
using Microsoft.Extensions.Configuration;

namespace Service;

public class EmailService(IConfiguration configuration)
{
    public async Task<bool> SendEmail(EmailRequest emailRequest, string subject, string body)
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
            throw;
        }
    }
}