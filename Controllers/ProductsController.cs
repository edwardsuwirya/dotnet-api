using Microsoft.AspNetCore.Mvc;
using MySimpleNetApi.Models;

namespace MySimpleNetApi.Controllers;

// Convention diberikan suffix Controller
// Extends dengan ControllerBase, karena jika di-extends dengan Controller
// Akan diberikan support ke Views (HTML, CSS, JS)
public class ProductsController : BaseController
{
    /* 3 pendekatan cara melakukan return dari API
     - Tipe data nya langsung => langsung status code 200
     - IActionResult => Bisa memberikan HTTP status code yang lain yang dibungkus dengan method, contoh: Ok(products), NotFound() 404
     - ActionResult<T> => Syantactical Sugar supaya return type dari API lebih clear
    */
    [HttpGet]
    public async Task<ActionResult<string>> GetAllProduct()
    {
        return "Hello Enigma 2.0";
    }

    [HttpGet("not-found")]
    public async Task<ActionResult<string>> GetProductButNotFound()
    {
        // Akan memberikan status 404
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> PutProduct(string id, [FromBody] Product product)
    {
        Console.WriteLine(id);
        Console.WriteLine(product.Id);
        if (id.Equals(product.Id) == false)
        {
            // Akan memberikan status 400
            return BadRequest();
        }
        else
        {
            return NoContent();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
    {
        return product;
    }
}