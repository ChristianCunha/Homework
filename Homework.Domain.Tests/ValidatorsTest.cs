using Homework.Domain.Errors;
using Homework.Domain.Validators;

namespace Homework.Domain.Tests
{
    public class ValidatorsTest
    {
        private readonly CalculatePurchaseValidator _sut;

        public ValidatorsTest()
        {
            _sut = new CalculatePurchaseValidator();
        }

        [Fact]
        public void ValidateCalculatePurchase_Should_Fail_InvalidValueVatRate()
        {
            var command = new CalculatePurchase(0, null, null, null);
            var result = _sut.Validate(command);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.VatRate));
        }

        [Fact]
        public void ValidateCalculatePurchase_Should_Fail_InvalidValueGross()
        {
            var command = new CalculatePurchase(0.1m, 0, null, null);
            var result = _sut.Validate(command);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Gross));
        }

        [Fact]
        public void ValidateCalculatePurchase_Should_Fail_InvalidValueVat()
        {
            var command = new CalculatePurchase(0.1m, null, 0, null);
            var result = _sut.Validate(command);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Vat));
        }

        [Fact]
        public void ValidateCalculatePurchase_Should_Fail_InvalidValueNet()
        {
            var command = new CalculatePurchase(0.1m, null, null, 0);
            var result = _sut.Validate(command);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Net));
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(1.0, 1.0, null)]
        [InlineData(null, 1.0, 1.0)]
        [InlineData(1.0, null, 1.0)]
        [InlineData(1.0, 1.0, 1.0)]
        public void ValidateCalculatePurchase_Should_Fail_MultipleValues(double? gross, double? vat, double? net)
        {
            var command = new CalculatePurchase(0.1m, (decimal?)gross, (decimal?)vat, (decimal?)net);
            var result = _sut.Validate(command);
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorCode == ValidationErrorCode.MultiplesFieldsInvalid.ErrorCode);
        }
    }
}