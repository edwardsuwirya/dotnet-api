using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Filter;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Console.WriteLine(context.ModelState.IsValid);
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(new CommonResponse<string>("400", "bad request body"));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}