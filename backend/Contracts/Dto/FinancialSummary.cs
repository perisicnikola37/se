namespace Contracts.Dto;
public record FinancialSummary
{
	public int ExpenseCount { get; set; }
	public int IncomeCount { get; set; }
}