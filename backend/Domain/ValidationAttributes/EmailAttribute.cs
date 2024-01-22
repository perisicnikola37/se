using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.ValidationAttributes;

public class EmailValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || !IsValidEmail(value.ToString()))
            return new ValidationResult(ErrorMessage ??
                                        $"The field {validationContext.DisplayName} must be a valid email address.");

        return ValidationResult.Success;
    }

    private static bool IsValidEmail(string? email)
    {
        if (email is null)
            return false;

        const string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
}