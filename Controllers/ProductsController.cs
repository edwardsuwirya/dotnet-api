using AutoMapper;
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
     - ActionResult<T> => Syantactical Sugar supaya return type dari API lebih clear
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
    public async Task<CommonResponse<ProductResponse>> PutProduct(string id, [FromBody] RegisterProductRequest product)
    {
        // Console.WriteLine(id);
        // Console.WriteLine(product.Id);
        if (string.IsNullOrEmpty(id))
        {
            // Akan memberikan status 400
            throw new BadRequestException("ID is needed");
        }

        var request = _mapper.Map<RegisterProductRequest, Product>(product);
        var result = await _productService.UpdateProduct(id, request);
        var response = _mapper.Map<Product, ProductResponse>(result);
        return new CommonResponse<ProductResponse>(response);
    }

    [HttpPost]
    public async Task<CommonResponse<ProductResponse>> PostProduct([FromBody] RegisterProductRequest product)
    {
        var request = _mapper.Map<RegisterProductRequest, Product>(product);
        var result = await _productService.RegisterProduct(request);
        var response = _mapper.Map<Product, ProductResponse>(result);
        return new CommonResponse<ProductResponse>(response);
    }

    [HttpDelete]
    public async Task<CommonResponse<string>> DeleteProduct(string id)
    {
        var result = await _productService.DeleteProduct(id);
        return new CommonResponse<string>(id);
    }
}