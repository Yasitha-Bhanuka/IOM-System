using Microsoft.AspNetCore.Mvc;
using IOMSystem.Application.Interfaces;
using IOMSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{sku}")]
    public async Task<IActionResult> GetBySku(string sku)
    {
        var product = await _productService.GetProductBySkuAsync(sku);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        var result = await _productService.CreateProductAsync(productDto);
        if (!result) return BadRequest("Error creating product. Check validation rules or duplicates.");
        return CreatedAtAction(nameof(GetBySku), new { sku = productDto.LocationCode + productDto.ProductID }, productDto);
    }

    [Authorize(Roles = "Manager")]
    [HttpPut("{sku}")]
    public async Task<IActionResult> Update(string sku, [FromBody] ProductDto productDto)
    {
        var result = await _productService.UpdateProductAsync(sku, productDto);
        if (!result) return BadRequest("Error updating product.");
        return NoContent();
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{sku}")]
    public async Task<IActionResult> Delete(string sku)
    {
        await _productService.DeleteProductAsync(sku);
        return NoContent();
    }
}
