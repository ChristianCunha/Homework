using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Homework.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected ActionResult CustomValidationProblem(List<Error> erros)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in erros)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }
    }
}
