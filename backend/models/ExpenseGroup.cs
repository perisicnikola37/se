public class ExpenseGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public ICollection<Expense> Expenses { get; set; }
}