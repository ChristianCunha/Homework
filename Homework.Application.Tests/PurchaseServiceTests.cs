using FluentValidation;
using FluentValidation.Results;
using Homework.Application.Services;
using Homework.Domain;
using Homework.Domain.Factory;
using Moq;

namespace Homework.Application.Tests
{
    public class PurchaseServiceTests
    {
        private readonly Mock<IPurchaseFactory> _purchaseFactoryMock;
        private readonly Mock<IValidator<CalculatePurchase>> _validatorMock;
        private readonly IPurchaseService _sut;

        public PurchaseServiceTests()
        {
            _purchaseFactoryMock = new Mock<IPurchaseFactory>();
            _validatorMock = new Mock<IValidator<CalculatePurchase>>();
            _sut = new PurchaseService(_purchaseFactoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public void Calculate_Should_ReturnErrors_ValidationFail()
        {
            //Arrange
            var command = new CalculatePurchase(0, null, null, null);
            var validationFailResult = new ValidationResult(new List<ValidationFailure> 
                                    {
                                        new ValidationFailure("Property","Error")
                                    });
            _validatorMock.Setup(m => m.Validate(It.IsAny<CalculatePurchase>())).Returns(() => validationFailResult);

            //Act
            var result = _sut.Calculate(command);

            //Assert
            _validatorMock.Verify(m => m.Validate(It.IsAny<CalculatePurchase>()), Times.Once);
            _purchaseFactoryMock.Verify(m => m.Create(It.IsAny<CalculatePurchase>()), Times.Never);
            Assert.True(result.IsError);
        }


        [Theory]
        [MemberData(nameof(ValidData))]
        public void Calculate_Should_ReturnExpectedValues_ValidationTrue(decimal vatRate, decimal? gross, decimal? vat, decimal? net, decimal grossResult, decimal vatResult, decimal netResult)
        {
            //Arrange
            var command = new CalculatePurchase(vatRate, gross, vat, net);
            _validatorMock.Setup(m => m.Validate(It.IsAny<CalculatePurchase>())).Returns(() => new ValidationResult());
            _purchaseFactoryMock.Setup(m => m.Create(command)).Returns(() =>
            {
                if (gross.HasValue) return new PurchaseGross(vatRate, gross.Value);
                if (vat.HasValue) return new PurchaseVat(vatRate, vat.Value);
                if (net.HasValue) return new PurchaseNet(vatRate, net.Value);
                return null;
            });

            //Act
            var result = _sut.Calculate(command);

            //Assert
            _validatorMock.Verify(m => m.Validate(It.IsAny<CalculatePurchase>()), Times.Once);
            _purchaseFactoryMock.Verify(m => m.Create(It.IsAny<CalculatePurchase>()), Times.Once);
            Assert.Equal(grossResult, Math.Round(result.Value.Gross,2));
            Assert.Equal(vatResult, Math.Round(result.Value.Vat, 2));
            Assert.Equal(netResult, Math.Round(result.Value.Net, 2));
        }

        public static IEnumerable<object[]> ValidData() =>
            new List<object[]>
                {
                    new object[] { 0.1m, 110m, null, null, 110, 10, 100 },
                    new object[] { 0.1m, null, 10m, null, 110, 10, 100 },
                    new object[] { 0.1m, null, null, 100m, 110, 10, 100 },
                    new object[] { 0.13m, 79.70m, null, null, 79.70m, 9.17m, 70.53m },
                    new object[] { 0.13m, null, 9.17m, null, 79.71m, 9.17m, 70.54m },
                    new object[] { 0.13m, null, null, 70.53m, 79.70m, 9.17m, 70.53m },
                };
    }
}