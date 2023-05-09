namespace Homework.Domain;
public record CalculatePurchase
    (decimal VatRate,
     decimal? Gross,
     decimal? Vat,
     decimal? Net);

