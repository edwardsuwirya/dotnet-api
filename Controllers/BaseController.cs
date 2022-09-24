using Microsoft.AspNetCore.Mvc;

namespace MySimpleNetApi.Controllers;

[Route("/api/[controller]")]
[Produces("application/json")]
// Global Filter using base controller, or we can put filter in every controller that needed
// [TypeFilter(typeof(ModelValidationFilter))]
// Automatic return error 400 when request model is invalid
// [ApiController]
public class BaseController : ControllerBase
{
}