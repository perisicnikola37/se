namespace Contracts.Dto;

public record IncomeDto
{
	public int? Id { get; set; }
	public string? Description { get; set; }
	public float? Amount { get; set; }
	public DateTime? CreatedAt { get; set; }
	public int? IncomeGroupId { get; set; }
	public int? UserId { get; set; }
	public string? Username { get; set; }
}

public record IncomeGroupDto
{
	public int? Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public IEnumerable<IncomeDto>? Incomes { get; set; }
}
