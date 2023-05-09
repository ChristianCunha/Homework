namespace Homework.Domain
{
    public class PurchaseNet : PurchaseBase, IPurchase
    {
        public PurchaseNet(decimal vatRate, decimal value) : base(vatRate, value)
        {
        }

        public PurchaseResult Calculate() => new PurchaseResult(Value + Value * VatRate, Value * VatRate, Value);
    }
}
