using System.ComponentModel.DataAnnotations;

namespace Homework.Api.DataAnnotations.cs
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class OptionalDecimalGreaterZero : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            return value is null || (decimal.TryParse(value.ToString(), out decimal output) && output > 0)
                ? ValidationResult.Success
                : new ValidationResult("The value of the field must be a number greater than 0.");
        }
    }
}
