using Contracts.Dto;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailRequest emailRequest, string subject, string body);
    }
}
