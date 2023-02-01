using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems);
    Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems);
    Task<ServiceResponse<int>> GetCartItemsCount();
    Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts();
    Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
    Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
    Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
}