namespace Domain.Validators;

using Domain.Models;
using FluentValidation;

public class ReminderValidator : AbstractValidator<Reminder>
{
    public ReminderValidator()
    {
        RuleFor(reminder => reminder.Type).NotNull().MinimumLength(2).MaximumLength(255);
        RuleFor(reminder => reminder.Active).NotNull();
    }
}
