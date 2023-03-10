using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Client.Services.CartService;

public interface ICartService
{
    event Action OnChanged;
    Task AddToCartAsync(CartItem cartItem);
    Task<List<CartProductResponse>> GetCartProductsAsync();
    Task RemoveProductFromCart(int productId,int productTypeId);
    Task UpdateQuantity(CartProductResponse product);
    Task StoreCartItems(bool emptyLocalCart);
    Task GetCartItemsCount();
}