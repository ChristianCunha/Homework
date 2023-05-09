namespace Homework.Api.Dtos
{
    public record PurchaseResponse(
        decimal Gross,
        decimal Vat,
        decimal Net);
}
