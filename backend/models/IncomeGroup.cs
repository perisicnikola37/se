public class IncomeGroup
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public ICollection<Income> Incomes { get; set; }
}