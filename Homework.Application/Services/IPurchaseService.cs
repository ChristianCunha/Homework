using ErrorOr;
using Homework.Domain;

namespace Homework.Application.Services
{
    public interface IPurchaseService
    {
        ErrorOr<CalculationResult> Calculate(CalculatePurchase command);
    }
}
