public class ExpenseGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<Expense> Expenses { get; set; }
}