namespace Contracts.Dto;

public class ExpenseResponseDto
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public float Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? ExpenseGroupId { get; set; }
    public ExpenseGroupDto ExpenseGroup { get; set; }
    public int UserId { get; set; }
    public UserResponseDto User { get; set; }
}
