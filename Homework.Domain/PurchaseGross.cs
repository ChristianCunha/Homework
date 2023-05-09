namespace Homework.Domain
{
    public class PurchaseGross : PurchaseBase, IPurchase
    {
        public PurchaseGross(decimal vatRate, decimal value) : base(vatRate, value)
        {
        }
        public PurchaseResult Calculate() => new PurchaseResult(Value, Value - Value / (1 + VatRate), Value / (1 + VatRate));
    }
}
