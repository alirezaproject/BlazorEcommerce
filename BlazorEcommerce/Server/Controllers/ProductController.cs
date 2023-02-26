using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("admin"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAdminProducts()
    {
        var result = await _productService.GetAdminProducts();
        return Ok(result);
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProducts(Product product)
    {
        var result = await _productService.CreateProduct(product);
        return Ok(result);
    }

    [HttpPut, Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        var result = await _productService.UpdateProduct(product);
        return Ok(result);
    }

    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProducts(int id)
    {
        var result = await _productService.DeleteProduct(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _productService.GetProductsAsync());
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var result = await _productService.GetProductAsync(productId);
        return Ok(result);
    }

    [HttpGet("category/{categoryUrl}")]
    public async Task<IActionResult> GetProduct(string categoryUrl)
    {
        
        var result = await _productService.GetProductsByCategory(categoryUrl);
        return Ok(result);
    }

    [HttpGet("search/{searchText}/{page:int}")]
    public async Task<IActionResult> SearchProducts(string searchText,int page =1)
    {

        var result = await _productService.SearchProducts(searchText,page);
        return Ok(result);
    }

    [HttpGet("searchSuggestions/{searchText}")]
    public async Task<IActionResult> GetProductSearchSuggestions(string searchText)
    {
        
        var result = await _productService.GetProductSearchSuggestions(searchText);
        return Ok(result);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedProducts()
    {

        var result = await _productService.GetFeaturedProducts();
        return Ok(result);
    }
}

