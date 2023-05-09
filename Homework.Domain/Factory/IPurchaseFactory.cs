using ErrorOr;

namespace Homework.Domain.Factory
{
    public interface IPurchaseFactory
    {
        IPurchase Create(CalculatePurchase command);
    }
}