using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class BlogValidator : AbstractValidator<Blog>
{
    public BlogValidator()
    {
        RuleFor(blog => blog.Description).NotNull().MinimumLength(8);
        RuleFor(blog => blog.Text).NotNull().MinimumLength(8).MaximumLength(255);
    }
}