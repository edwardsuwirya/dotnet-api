using System.ComponentModel.DataAnnotations;

namespace MySimpleNetApi.Resource;

public class UpdateProductRequest
{
    [Required] public string ProductName { get; set; }
}