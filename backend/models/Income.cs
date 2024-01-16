public class Income
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public IncomeGroup IncomeGroup { get; set; }
}