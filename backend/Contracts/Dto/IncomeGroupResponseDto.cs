using Contracts.Dto;

public class IncomeGroupResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public UserDto? User { get; set; }
    public List<IncomeDto>? Incomes { get; set; }
}
