namespace Domain.Validators;

using Models;
using FluentValidation;

public class ExpenseGroupValidator : AbstractValidator<ExpenseGroup>
{
	public ExpenseGroupValidator()
	{
		RuleFor(expenseGroup => expenseGroup.Name).NotNull().MinimumLength(2).MaximumLength(255);
		RuleFor(expenseGroup => expenseGroup.Description).NotNull().MinimumLength(8).MaximumLength(255);
	}
}
