using System.ComponentModel.DataAnnotations;

namespace MySimpleNetApi.Resource;

public class RegisterCategoryRequest
{
    [Required] public string CategoryName { get; set; }
}