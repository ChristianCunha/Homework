using Homework.Api.Dtos;
using Homework.Application.Services;
using Homework.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Api.Controllers
{
    [ApiController]
    [Route("api/v1/purchases")]
    public class PurchasesController : ApiControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        //Reason: HttpGet
        //The API Rest standards are designed around resources, but in this case, we have an action.
        //In an action we can use GET or POST, as we have a simple payload and given that calculations are idempotent and browser may cache these request, I chose to use GET

        //Reason: Synchronous
        //All operations in API are synchronous, we don't have any reposirtory, message broker or resource with a async methods

        //Reason: PurchaseResquest using decimal
        //I chose to use a strong typed object for request defining decimal instead string.
        //Defining a type in request object keep more clear e readeble in openAPI documentation

        [HttpGet]
        [Route("calculate")]
        [ProducesResponseType(typeof(PurchaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<PurchaseResponse> Calculate([FromQuery] PurchaseRequest request)
        {
            //Reason: DataAnnotations
            //I used a DataAnnotation to validate de model to demonstrate this aproach and create a custom ValidationAttribute
            //I could used a validation library as FluentValidation like I used in domain layer

            var purchase = new CalculatePurchase(request.VatRate, request.Gross, request.Vat, request.Net);

            //Reason: Service layer Pattern
            //In this simples case, I used service layer pattern, but we could have used a mediator like a MediatR library
            //to send querys and commands to application layer 

            var calculateResult = _purchaseService.Calculate(purchase);

            return calculateResult.Match<ActionResult<PurchaseResponse>>(
                result => Ok(new PurchaseResponse(Math.Round(result.Gross, 2), Math.Round(result.Vat, 2), Math.Round(result.Net, 2))),
                errors => CustomValidationProblem(errors));
        }
    }
}
