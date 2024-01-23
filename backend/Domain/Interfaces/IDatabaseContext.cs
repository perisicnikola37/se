using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IDatabaseContext
{
    DbSet<User> Users { get; set; }
    DbSet<Blog> Blogs { get; set; }
    DbSet<ExpenseGroup> ExpenseGroups { get; set; }
    DbSet<Expense> Expenses { get; set; }
    DbSet<IncomeGroup> IncomeGroups { get; set; }
    DbSet<Income> Incomes { get; set; }
    DbSet<Reminder> Reminders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}