namespace Homework.Domain.Factory
{
    public class PurchaseFactory : IPurchaseFactory
    {
        public PurchaseFactory() { }

        public IPurchase Create(CalculatePurchase command)
        {
            return command switch
            {
                { Gross: > 0 } => new PurchaseGross(command.VatRate, command.Gross.Value),
                { Vat: > 0 } => new PurchaseVat(command.VatRate, command.Vat.Value),
                { Net: > 0 } => new PurchaseNet(command.VatRate, command.Net.Value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}