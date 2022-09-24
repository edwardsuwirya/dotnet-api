using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Filter;
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
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper
    )
    {
        _productService = productService;
        _mapper = mapper;
    }

    /* 3 pendekatan cara melakukan return dari API
     - Tipe data nya langsung => langsung status code 200
     - IActionResult => Bisa memberikan HTTP status code yang lain yang dibungkus dengan method, contoh: Ok(products), NotFound() 404
     - ActionResult<T> => Syntactical Sugar supaya return type dari API lebih clear
    */
    [HttpGet]
    public async Task<CommonResponse<List<ProductResponse>>> GetAllProduct()
    {
        var result = await _productService.List();
        var response = _mapper.Map<List<Product>, List<ProductResponse>>(result);
        return new CommonResponse<List<ProductResponse>>(response);
    }

    [HttpGet("not-found")]
    public async Task<ActionResult<string>> GetProductButNotFound()
    {
        // Akan memberikan status 404
        return NotFound();
    }


    [HttpPut("{id}")]
    [ServiceFilter(typeof(EntityExistsValidationFilter))]
    [TypeFilter(typeof(ModelValidationFilter))]
    public async Task<CommonResponse<ProductResponse>> PutProduct([FromRoute] string id,
        [FromBody] UpdateProductRequest product)
    {
        // Console.WriteLine(id);
        // Console.WriteLine(product.Id);
        var existingProduct = HttpContext.Items["entity"] as Product;
        var request = _mapper.Map(product, existingProduct);
        var result = await _productService.UpdateProduct(request);
        var response = _mapper.Map<Product, ProductResponse>(result);
        return new CommonResponse<ProductResponse>(response);
    }

    [HttpPost]
    [TypeFilter(typeof(ModelValidationFilter))]
    public async Task<CommonResponse<ProductResponse>> PostProduct([FromBody] RegisterProductRequest product)
    {
        var request = _mapper.Map<RegisterProductRequest, Product>(product);
        var result = await _productService.RegisterProduct(request);
        var response = _mapper.Map<Product, ProductResponse>(result);
        return new CommonResponse<ProductResponse>(response);
    }

    [HttpDelete("{id}")]
    [ServiceFilter(typeof(EntityExistsValidationFilter))]
    public async Task<CommonResponse<string>> DeleteProduct([FromRoute] string id)
    {
        var existingProduct = HttpContext.Items["entity"] as Product;
        var result = await _productService.DeleteProduct(existingProduct);
        return new CommonResponse<string>(id);
    }
}