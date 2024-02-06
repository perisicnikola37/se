namespace Contracts.Dto;

public record ForgotPasswordRequestDto
{
	public string UserEmail { get; set; }
}