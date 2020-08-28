namespace WebApi.Filters
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Application.Common.Models;

    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) 
                return;

            Dictionary<string, List<string>> errorList = new Dictionary<string, List<string>>();

            foreach (string key in context.ModelState.Keys)
            {
                var property = context.ModelState.GetValueOrDefault(key);

                List<string> errors = property.Errors.Select(error => error.ErrorMessage).ToList();

                errorList.Add(key,errors);
            }

            context.Result = new BadRequestObjectResult(ServiceResult.Failed(errorList, ServiceError.ValidationFormat));
        }
    }
}
