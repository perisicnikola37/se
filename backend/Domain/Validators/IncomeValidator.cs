namespace Domain.Validators;

using Domain.Models;
using FluentValidation;

public class IncomeValidator : AbstractValidator<Income>
{
	public IncomeValidator()
	{
		RuleFor(income => income.Description).NotNull().MinimumLength(8).MaximumLength(255);
		RuleFor(income => income.Amount).NotNull().GreaterThan(0);
		RuleFor(income => income.IncomeGroupId).NotNull();
	}
}
