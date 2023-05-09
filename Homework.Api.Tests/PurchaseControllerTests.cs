using ErrorOr;
using Homework.Api.Controllers;
using Homework.Api.Dtos;
using Homework.Application.Services;
using Homework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Homework.Api.Tests
{
    public class PurchaseControllerTests
    {
        private readonly Mock<IPurchaseService> _purchaseServiceMock;
        private readonly PurchasesController _sut;

        public PurchaseControllerTests()
        {
            _purchaseServiceMock = new Mock<IPurchaseService>();
            _sut = new PurchasesController(_purchaseServiceMock.Object);
        }

        [Fact]
        public void Calculate_WithValidationFail_Should_ReturnBadRequest()
        {
            //Arrange
            var request = new PurchaseRequest(0, 0, 0, 0);
            _purchaseServiceMock.Setup(m => m.Calculate(It.IsAny<CalculatePurchase>())).Returns(() => Error.Validation());

            //Act
            var result = _sut.Calculate(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            var problemDetail = objectResult.Value as ValidationProblemDetails;
            Assert.NotNull(problemDetail);
        }

        [Theory]
        [InlineData(0.1, 110.0, null, null, 110, 10, 100)]
        [InlineData(0.1, null, 10.0, null, 110, 10, 100)]
        [InlineData(0.1, null, null, 100.0, 110, 10, 100)]
        [InlineData(0.13, 113.0, null, null, 113, 13, 100)]
        [InlineData(0.13, null, 13.0, null, 113, 13, 100)]
        [InlineData(0.13, null, null, 100.0, 113, 13, 100)]
        public void Calculate_WithValidData_Should_ReturnCorrectResults(double vatRate, double? gross, double? vat, double? net, decimal grossResult, decimal vatResult, decimal netResult)
        {

            //Arrange
            var request = new PurchaseRequest((decimal)vatRate, (decimal?)gross, (decimal?)vat, (decimal?)net);
            var command = new CalculatePurchase(request.VatRate, request.Gross, request.Vat, request.Net);
            _purchaseServiceMock.Setup(m => m.Calculate(command)).Returns(new CalculationResult(grossResult, vatResult, netResult));

            //Act
            var result = _sut.Calculate(request);

            //Assert
            _purchaseServiceMock.Verify(m => m.Calculate(It.IsAny<CalculatePurchase>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
            var objectResult = result.Result as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            var response = objectResult.Value as PurchaseResponse;
            Assert.NotNull(response);
            Assert.Equal(grossResult, response.Gross);
            Assert.Equal(vatResult, response.Vat);
            Assert.Equal(netResult, response.Net);
        }
    }
}