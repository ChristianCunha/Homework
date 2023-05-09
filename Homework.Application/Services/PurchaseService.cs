using ErrorOr;
using FluentValidation;
using Homework.Domain;
using Homework.Domain.Factory;

namespace Homework.Application.Services
{
    public class PurchaseService : IPurchaseService
    {

        private readonly IPurchaseFactory _purchaseFactory;
        private readonly IValidator<CalculatePurchase> _validator;

        public PurchaseService(IPurchaseFactory purchaseFactory, IValidator<CalculatePurchase> validator)
        {
            _purchaseFactory = purchaseFactory;
            _validator = validator;
        }

        public ErrorOr<CalculationResult> Calculate(CalculatePurchase command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
            }

            var purchase = _purchaseFactory.Create(command);
            var result = purchase.Calculate();

            //Reason: Do not round
            //Even though our compare function only has two decimal places,
            //I decided not to round because this same service can be used for other purposes.
            //Even for a sequence of calculations and rounding could impact the final result.
            //In this way, only the presentation call will round
            return new CalculationResult(result.Gross, result.Vat, result.Net);

        }
    }
}
