using System.ComponentModel.DataAnnotations;

namespace MySimpleNetApi.Resource;

public class RegisterProductRequest
{
    [Required] public string ProductName { get; set; }

    [Required] public string CategoryId { get; set; }
}