using Shared.Entities.Common;
using Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Validators
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(new ServiceResponse(Constants.ValidationErrorCode, string.Join("</br>", errors)));
                return;
            }
            await next();
        }
    }

}
