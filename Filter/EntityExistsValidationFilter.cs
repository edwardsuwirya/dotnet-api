using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Repository;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Filter;

public class EntityExistsValidationFilter : IActionFilter
{
    private readonly AppDbContext _context;
    public EntityExistsValidationFilter(AppDbContext context) => _context = context;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.ContainsKey("id"))
        {
            string id = (string)context.ActionArguments["id"];
            var entity = _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));
            var existingProduct = entity.Result;
            if (existingProduct == null)
            {
                throw new NotFoundException("Product Not Exist");
            }
            else
            {
                context.HttpContext.Items.Add("entity", existingProduct);
            }
        }
        else
        {
            context.Result = new BadRequestObjectResult(new CommonResponse<string>("400", "bad request parameter"));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}