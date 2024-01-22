namespace Domain.Models;

public class Expense
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public float Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int? ExpenseGroupId { get; set; }
    public ExpenseGroup? ExpenseGroup { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}