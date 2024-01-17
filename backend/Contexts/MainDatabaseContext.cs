namespace Vega.Classes;

using Microsoft.EntityFrameworkCore;
using Vega.Interfaces;
using Vega.Models;

public class MainDatabaseContext : DbContext, IMainDatabaseContext
{
    public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ExpenseGroup> Expense_groups { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<IncomeGroup> Income_groups { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Reminder> Reminders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.ExpenseGroup)
            .WithMany(g => g.Expenses)
            .HasForeignKey(e => e.ExpenseGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Income>()
            .HasOne(e => e.User)
            .WithMany(u => u.Incomes)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Income>()
            .HasOne(i => i.IncomeGroup)
            .WithMany(g => g.Incomes)
            .HasForeignKey(i => i.IncomeGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}