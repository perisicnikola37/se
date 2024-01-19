using System.ComponentModel.DataAnnotations;

namespace Domain.ValidationAttributes;
    public class MinimumAttribute : ValidationAttribute
    {
        private readonly double _minValue;

        public MinimumAttribute(double minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null || Convert.ToDouble(value) <= _minValue)
            {
                return new ValidationResult(ErrorMessage ?? $"The field {validationContext.DisplayName} must be greater than {_minValue}.");
            }

            return ValidationResult.Success;
        }
    }
