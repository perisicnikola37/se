namespace Domain.Validators;

using Models;
using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
	public UserValidator()
	{
		RuleFor(user => user.Username).NotNull().MinimumLength(2).MaximumLength(255);
		RuleFor(user => user.Email).NotNull().EmailAddress().MinimumLength(2).MaximumLength(255);
		RuleFor(user => user.Password).NotEmpty().WithMessage("Your password cannot be empty")
				   .MinimumLength(8).WithMessage("Your password length must be at least 8.")
				   .MaximumLength(36).WithMessage("Your password length must not exceed 36.")
				   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
				   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
				   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
				   .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
	}
}
