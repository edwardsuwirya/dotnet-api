using Microsoft.AspNetCore.Mvc;

namespace MySimpleNetApi.Controllers;

[Route("/api/[controller]")]
[Produces("application/json")]
[ApiController]
public class BaseController : ControllerBase
{
}