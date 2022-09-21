using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Models;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Controllers;

[Route("/api/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<CommonResponse<List<Category>>> GetAllCategories()
    {
        return new CommonResponse<List<Category>>
        {
            StatusCode = "00",
            Message = "Success",
            Data = new List<Category>
            {
                new Category
                {
                    Id = "1",
                    CategoryName = "Food"
                },
                new Category
                {
                    Id = "2",
                    CategoryName = "Beverage"
                }
            }
        };
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<CommonResponse<Category>> PostCategory([FromBody] Category category)
    {
        Console.WriteLine(category.CategoryName);
        return new CommonResponse<Category>
        {
            StatusCode = "00",
            Message = "Success",
            Data = category
        };
    }
}