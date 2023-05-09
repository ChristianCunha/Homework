namespace Homework.Application.Services
{
    public record CalculationResult(
        decimal Gross,
        decimal Vat,
        decimal Net);
}
