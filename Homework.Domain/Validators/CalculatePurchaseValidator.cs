using FluentValidation;
using Homework.Domain.Errors;

namespace Homework.Domain.Validators
{
    public class CalculatePurchaseValidator : AbstractValidator<CalculatePurchase>
    {
        public CalculatePurchaseValidator()
        {
            RuleFor(p => p.VatRate).Must(value => ValidRate(value)).WithMessage(string.Format(ValidationErrorCode.VatRateInvalid.ErrrorMessage, string.Join(" or ", GetRates()))).WithErrorCode(ValidationErrorCode.VatRateInvalid.ErrorCode);
            RuleFor(p => p).Must(t => ValidCombination(t)).WithMessage(ValidationErrorCode.MultiplesFieldsInvalid.ErrrorMessage).WithName(ValidationErrorCode.MultiplesFieldsInvalid.PropertyName).WithErrorCode(ValidationErrorCode.MultiplesFieldsInvalid.ErrorCode);
            RuleFor(p => p.Gross).Must(p => p.HasValue && p.Value > 0).When(p => p.Gross.HasValue).WithMessage(ValidationErrorCode.GrossInvalid.ErrrorMessage).WithErrorCode(ValidationErrorCode.GrossInvalid.ErrorCode); ;
            RuleFor(p => p.Vat).Must(p => p.HasValue && p.Value > 0).When(p => p.Vat.HasValue).WithMessage(ValidationErrorCode.VatInvalid.ErrrorMessage).WithErrorCode(ValidationErrorCode.VatInvalid.ErrorCode); ;
            RuleFor(p => p.Net).Must(p => p.HasValue && p.Value > 0).When(p => p.Net.HasValue).WithMessage(ValidationErrorCode.NetInvalid.ErrrorMessage).WithErrorCode(ValidationErrorCode.NetInvalid.ErrorCode); ;
        }

        private bool ValidCombination(CalculatePurchase instance)
        {
            var grossHasValue = instance.Gross != null && instance.Gross > 0 ? 1 : 0;
            var vatHasValue = instance.Vat != null && instance.Vat > 0 ? 1 : 0;
            var netHasValue = instance.Net != null && instance.Net > 0 ? 1 : 0;
            if (grossHasValue + vatHasValue + netHasValue != 1)
            {
                return false;
            }
            return true;
        }

        private bool ValidRate(decimal value)
        {
            if (!GetRates().ToList().Contains(value))
            {
                return false;
            }
            return true;
        }

        //It could be retrieved from a repository or a external source
        private IEnumerable<decimal> GetRates() => new List<decimal> { 0.1m, 0.13m, 0.2m };
    }
}
