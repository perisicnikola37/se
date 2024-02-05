namespace Domain.Models;

public class ExpenseGroup
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public int UserId { get; set; }
	public User? User { get; set; }
	public List<Expense>? Expenses { get; set; }
}