namespace Contracts.Dto;

public class IncomeGroupResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public UserDto? User { get; set; }
    public List<IncomeDto>? Incomes { get; set; }
}
