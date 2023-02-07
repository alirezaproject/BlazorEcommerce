using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductTypes()
        {
            var response = await _productTypeService.GetProductTypes();
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProductTypes(ProductType productType)
        {
            var response = await _productTypeService.AddProductType(productType);
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateProductTypes(ProductType productType)
        {
            var response = await _productTypeService.UpdateProductType(productType);
            return Ok(response);
        }
    }
}
