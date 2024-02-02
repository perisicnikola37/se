namespace Domain.Models;

public class IncomeGroup
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public int UserId { get; set; }
	public User? User { get; set; }
	public List<Income>? Incomes { get; set; }
}
