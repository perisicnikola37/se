namespace Contracts.Dto;

public class UserDto
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string AccountType { get; set; }
	public string FormattedCreatedAt => CreatedAt.ToString("dd-MM-yyyy");
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public string Token { get; set; }
}
