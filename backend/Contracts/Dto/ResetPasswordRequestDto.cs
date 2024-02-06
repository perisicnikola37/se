namespace Contracts.Dto;

public record ResetPasswordRequestDto
{
	public string UserEmail { get; set; }
	public string ResetToken { get; set; }
	public string NewPassword { get; set; }
}
