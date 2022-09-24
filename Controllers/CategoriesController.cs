using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Filter;
using MySimpleNetApi.Models;
using MySimpleNetApi.Resource;
using MySimpleNetApi.Services;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Controllers;

public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<CommonResponse<List<CategoryResponse>>> GetAllCategories()
    {
        // throw new Exception("Ooops");
        // throw new NotFoundException("Category is not found");
        var result = await _categoryService.List();
        var response = _mapper.Map<List<Category>, List<CategoryResponse>>(result);
        return new CommonResponse<List<CategoryResponse>>(response);
    }

    [TypeFilter(typeof(ModelValidationFilter))]
    [HttpPost]
    public async Task<CommonResponse<CategoryResponse>> PostCategory([FromBody] RegisterCategoryRequest category)
    {
        var request = _mapper.Map<RegisterCategoryRequest, Category>(category);
        var result = await _categoryService.RegisterCategory(request);
        var response = _mapper.Map<Category, CategoryResponse>(result);
        return new CommonResponse<CategoryResponse>(response);
    }
}