namespace Domain.Validators;

using Models;
using FluentValidation;

public class ExpenseValidator : AbstractValidator<Expense>
{
	public  ExpenseValidator()
	{
		RuleFor(expense => expense.Description).NotNull().MinimumLength(8).MaximumLength(255);
		RuleFor(expense => expense.Amount).NotNull().GreaterThan(0);
		RuleFor(expense => expense.ExpenseGroupId).NotNull();
	}
}
