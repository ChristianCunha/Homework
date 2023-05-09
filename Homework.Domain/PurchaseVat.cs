namespace Homework.Domain
{
    public class PurchaseVat : PurchaseBase, IPurchase
    {
        public PurchaseVat(decimal vatRate, decimal value) : base(vatRate, value)
        {
        }

        public PurchaseResult Calculate() => new PurchaseResult(Value / VatRate + Value, Value, Value / VatRate);
    }
}
