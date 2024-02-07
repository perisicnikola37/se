namespace Contracts.Dto;

public class IncomeResponseDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? IncomeGroupId { get; set; }
    public IncomeGroupDto IncomeGroup { get; set; }
    public int UserId { get; set; }
    public UserResponseDto User { get; set; }
}
