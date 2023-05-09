namespace Homework.Domain
{
    public abstract class PurchaseBase
    {
        protected decimal VatRate { get; }
        protected decimal Value { get; }

        public PurchaseBase(decimal vatRate, decimal value)
        {
            VatRate = vatRate;
            Value = value;
        }
    }

}
