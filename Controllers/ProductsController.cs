using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;
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
    public async Task<CommonResponse<List<Product>>> GetAllProduct()
    {
        var result = await _productService.List();
        return new CommonResponse<List<Product>>
        {
            StatusCode = "00",
            Message = "Success",
            Data = result
        };
    }

    [HttpGet("not-found")]
    public async Task<ActionResult<string>> GetProductButNotFound()
    {
        // Akan memberikan status 404
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<CommonResponse<Product>> PutProduct(string id, [FromBody] Product product)
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
            var result = await _productService.UpdateProduct(id, product);
            return new CommonResponse<Product>
            {
                StatusCode = "00",
                Message = "Success",
                Data = result
            };
        }
    }

    [HttpPost]
    public async Task<CommonResponse<Product>> PostProduct([FromBody] Product product)
    {
        var result = await _productService.RegisterProduct(product);
        return new CommonResponse<Product>
        {
            StatusCode = "00",
            Message = "Success",
            Data = result
        };
    }

    [HttpDelete]
    public async Task<CommonResponse<Product>> DeleteProduct(string id)
    {
        var result = await _productService.DeleteProduct(id);
        return new CommonResponse<Product>
        {
            StatusCode = "00",
            Message = "Success",
            Data = result
        };
    }
}