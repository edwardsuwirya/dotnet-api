using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;
using MySimpleNetApi.Resource;
using MySimpleNetApi.Services;
using MySimpleNetApi.Utils;

namespace MySimpleNetApi.Controllers;

// Convention diberikan suffix Controller
// Extends dengan ControllerBase, karena jika di-extends dengan Controller
// Akan diberikan support ke Views (HTML, CSS, JS)
[Authorize]
public class ProductsController : BaseController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) => _productService = productService;

    /* 3 pendekatan cara melakukan return dari API
     - Tipe data nya langsung => langsung status code 200
     - IActionResult => Bisa memberikan HTTP status code yang lain yang dibungkus dengan method, contoh: Ok(products), NotFound() 404
     - ActionResult<T> => Syantactical Sugar supaya return type dari API lebih clear
    */
    [HttpGet]
    public async Task<CommonResponse<List<ProductResponse>>> GetAllProduct()
    {
        var result = await _productService.List();
        var response = result.Select(p => new ProductResponse
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Category = new CategoryResponse
            {
                Id = p.Category.Id,
                CategoryName = p.Category.CategoryName
            }
        }).ToList();
        return new CommonResponse<List<ProductResponse>>
        {
            StatusCode = "00",
            Message = "Success",
            Data = response
        };
    }

    [HttpGet("not-found")]
    public async Task<ActionResult<string>> GetProductButNotFound()
    {
        // Akan memberikan status 404
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<CommonResponse<ProductResponse>> PutProduct(string id, [FromBody] RegisterProductRequest product)
    {
        // Console.WriteLine(id);
        // Console.WriteLine(product.Id);
        if (string.IsNullOrEmpty(id))
        {
            // Akan memberikan status 400
            throw new BadRequestException("ID is needed");
        }
        else
        {
            var result = await _productService.UpdateProduct(id, new Product
            {
                ProductName = product.ProductName,
                CategoryId = product.CategoryId
            });
            return new CommonResponse<ProductResponse>
            {
                StatusCode = "00",
                Message = "Success",
                Data = new ProductResponse
                {
                    Id = result.Id,
                    ProductName = result.ProductName,
                    Category = new CategoryResponse
                    {
                        Id = result.Category.Id,
                        CategoryName = result.Category.CategoryName
                    }
                }
            };
        }
    }

    [HttpPost]
    public async Task<CommonResponse<ProductResponse>> PostProduct([FromBody] RegisterProductRequest product)
    {
        var result = await _productService.RegisterProduct(new Product
        {
            ProductName = product.ProductName,
            CategoryId = product.CategoryId
        });
        return new CommonResponse<ProductResponse>
        {
            StatusCode = "00",
            Message = "Success",
            Data = new ProductResponse
            {
                Id = result.Id,
                ProductName = result.ProductName,
            }
        };
    }

    [HttpDelete]
    public async Task<CommonResponse<string>> DeleteProduct(string id)
    {
        var result = await _productService.DeleteProduct(id);
        return new CommonResponse<string>
        {
            StatusCode = "00",
            Message = "Success",
            Data = id
        };
    }
}