using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DatabaseContext : DbContext, IDatabaseContext
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

	// important for tests
	public DatabaseContext()
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Blog> Blogs { get; set; }
	public DbSet<ExpenseGroup> ExpenseGroups { get; set; }
	public DbSet<Expense> Expenses { get; set; }
	public DbSet<IncomeGroup> IncomeGroups { get; set; }
	public DbSet<Income> Incomes { get; set; }
	public DbSet<Reminder> Reminders { get; set; }

	public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await base.SaveChangesAsync(cancellationToken);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		try
		{
			SeedData.Seed(modelBuilder);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Blogs)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.IsRequired();

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

			modelBuilder.Entity<IncomeGroup>()
	  			.HasOne(ig => ig.User)
	  			.WithMany(u => u.IncomeGroups)
				.HasForeignKey(ig => ig.UserId)
	 			.IsRequired()
	  			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ExpenseGroup>()
			 	.HasOne(ig => ig.User)
			 	.WithMany(u => u.ExpenseGroups)
		 	 	.HasForeignKey(ig => ig.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred during model building: {ex.Message}");
			throw new OnModelCreatingException("DatabaseContext.cs");
		}
	}
}