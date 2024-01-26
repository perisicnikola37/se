using Contracts.Dto;

namespace Service
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailRequest emailRequest, string subject, string body);
    }
}
