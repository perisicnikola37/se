using System;
using NuGet.Packaging.Signing;

public class Expense
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public int Amount { get; set; }
    // this is unnecessary
    // public int ExpenseGroupId { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;

    public int ExpenseGroupId { get; set; }

    public ExpenseGroup? ExpenseGroup { get; set; }
}
