using BlazorEcommerce.Server.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;


        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<IActionResult> GetCartProducts(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProductsAsync(cartItems);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> StoreCartItems(List<CartItem> cartItems)
        {
            var result = await _cartService.StoreCartItems(cartItems);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCartItemsCount()
        {
            return Ok(await _cartService.GetCartItemsCount());
        }

        [HttpGet]
        public async Task<IActionResult> GetDbCartProducts()
        {
            return Ok(await _cartService.GetDbCartProducts());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(CartItem cartItem)
        {
            return Ok(await _cartService.AddToCart(cartItem));
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(CartItem cartItem)
        {
            return Ok(await _cartService.UpdateQuantity(cartItem));
        }

        [HttpDelete("{productId:int}/{productTypeId:int}")]
        public async Task<IActionResult> RemoveItemFromCart(int productId,int productTypeId)
        {
            return Ok(await _cartService.RemoveItemFromCart(productId,productTypeId));
        }
    }
}
