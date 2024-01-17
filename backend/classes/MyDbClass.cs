namespace Vega.classes;

using Microsoft.EntityFrameworkCore;
using Vega.interfaces;

public class MyDBContext : DbContext, IMyDBContext
{
    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ExpenseGroup> Expense_groups { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<IncomeGroup> Income_groups { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Reminder> Reminders { get; set; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}