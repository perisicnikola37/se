namespace Contracts.Dto;

public record ResetPasswordRequestDto
{
	public string UserEmail { get; set; } = default!;
	public string ResetToken { get; set; } = default!;
	public string NewPassword { get; set; } = default!;
}
