public class Income
{
    public int Id { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;

    public int IncomeGroupId { get; set; }

    public IncomeGroup? IncomeGroup { get; set; }
}