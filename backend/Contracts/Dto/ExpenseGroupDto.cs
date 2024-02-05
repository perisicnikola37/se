namespace Contracts.Dto;

public record ExpenseDto
{
	public int? Id { get; set; }
	public string? Description { get; set; }
	public float? Amount { get; set; }
	public DateTime? CreatedAt { get; set; }
	public int? ExpenseGroupId { get; set; }
	public int? UserId { get; set; }
	public string? Username { get; set; }
}

public record ExpenseGroupDto
{
	public int? Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public IEnumerable<ExpenseDto>? Expenses { get; set; }
}
