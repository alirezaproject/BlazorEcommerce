using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    public CartService(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }


    public event Action OnChanged = null!;

    public async Task AddToCartAsync(CartItem cartItem)
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();

        cart.Add(cartItem);
        await _localStorageService.SetItemAsync("cart", cart);
        OnChanged.Invoke();

    }

    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();

        return cart;
    }

    public async Task<List<CartProductResponse>> GetCartProductsAsync()
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");

        var response = await _httpClient.PostAsJsonAsync("api/Cart/products", cart);

        var cartProducts =
            await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
        return cartProducts?.Data!;
    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");


        var cartItem = cart?.Find(x => x.ProductTypeId == productTypeId && x.ProductId == productId);
        if (cartItem == null)
            return;

        cart?.Remove(cartItem);
        await _localStorageService.SetItemAsync("cart", cart);
        OnChanged.Invoke();

    }
}