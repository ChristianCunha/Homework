using Homework.Api.DataAnnotations.cs;
using System.ComponentModel.DataAnnotations;

namespace Homework.Api.Dtos
{
    public record PurchaseRequest(
        [Required]
        [OptionalDecimalGreaterZero]
        decimal VatRate,
        [OptionalDecimalGreaterZero]
        decimal? Gross,
        [OptionalDecimalGreaterZero]
        decimal? Vat,
        [OptionalDecimalGreaterZero]
        decimal? Net);
}
