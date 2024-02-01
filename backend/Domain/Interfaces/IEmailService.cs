using Contracts.Dto;

namespace Domain.Interfaces;

public interface IEmailService
{
	Task<bool> SendEmail(EmailRequestDto emailRequest, string subject, string body);
	Task<bool> SendEmailWithAttachment(EmailRequestDto emailRequest, string subject, string body, string attachmentFileName, byte[] attachmentContent, string attachmentContentType);
}