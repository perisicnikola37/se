namespace Domain.Models;

public class Income
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public float Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int IncomeGroupId { get; set; }
    public IncomeGroup? IncomeGroup { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}