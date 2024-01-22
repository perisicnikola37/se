using Domain.Models;
using FluentValidation;

public class BlogValidator : AbstractValidator<Blog>
{
  public BlogValidator()
  {
    RuleFor(blog => blog.Description).NotNull();
  }
}