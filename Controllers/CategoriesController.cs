using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;
using MySimpleNetApi.Resource;
using MySimpleNetApi.Services;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Controllers;

public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

    [HttpGet]
    public async Task<CommonResponse<List<CategoryResponse>>> GetAllCategories()
    {
        // throw new Exception("Ooops");
        // throw new NotFoundException("Category is not found");
        var result = await _categoryService.List();
        var response = result.Select(c => new CategoryResponse
        {
            Id = c.Id,
            CategoryName = c.CategoryName
        }).ToList();
        return new CommonResponse<List<CategoryResponse>>
        {
            StatusCode = "00",
            Message = "Success",
            Data = response
        };
    }

    [HttpPost]
    public async Task<CommonResponse<CategoryResponse>> PostCategory([FromBody] RegisterCategoryRequest category)
    {
        var result = await _categoryService.RegisterCategory(new Category
        {
            CategoryName = category.CategoryName
        });
        return new CommonResponse<CategoryResponse>
        {
            StatusCode = "00",
            Message = "Success",
            Data = new CategoryResponse
            {
                Id = result.Id,
                CategoryName = result.CategoryName
            }
        };
    }
}