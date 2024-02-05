namespace Domain.Validators;

using Domain.Models;
using FluentValidation;

public class BlogValidator : AbstractValidator<Blog>
{
	public BlogValidator()
	{
		RuleFor(blog => blog.Description).NotNull().MinimumLength(8);
		RuleFor(blog => blog.Text).NotNull().MinimumLength(8).MaximumLength(255);
	}
}
