using System;
using System.Net;
using System.Net.Mail;

public class EmailService
{
    public async Task<bool> SendEmail(EmailRequest emailRequest)
    {
        try
        {
            // TO DO: Make these credentials in appsettings.json
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("perisicnikola37@gmail.com", "secret"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = "Subject of the email",
                Body = "Body of the email",
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


