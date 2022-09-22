using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Controllers;

public class CategoriesController : BaseController
{
    [HttpGet]
    public async Task<CommonResponse<List<Category>>> GetAllCategories()
    {
        try
        {
            throw new Exception("Ooops");
            // throw new NotFoundException("Category is not found");
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
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