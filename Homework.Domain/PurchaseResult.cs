namespace Homework.Domain
{
    public record PurchaseResult(
        decimal Gross,
        decimal Vat,
        decimal Net
        )
    {
    }
}
