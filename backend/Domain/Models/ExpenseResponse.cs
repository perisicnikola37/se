using Domain.Models;

public class ExpenseResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? ExpenseGroupId { get; set; }
    public ExpenseGroup ExpenseGroup { get; set; }
    public int UserId { get; set; }
    public UserResponse User { get; set; }
}

