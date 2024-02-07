namespace Domain.Models;

public class IncomeResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? IncomeGroupId { get; set; }
    public IncomeGroup IncomeGroup { get; set; }
    public int UserId { get; set; }
    public UserResponse User { get; set; }
}
