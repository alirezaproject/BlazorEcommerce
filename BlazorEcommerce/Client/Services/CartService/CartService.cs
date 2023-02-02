using BlazorEcommerce.Shared;
using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;


    public CartService(ILocalStorageService localStorageService, HttpClient httpClient, IAuthService authService)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
        _authService = authService;
    }


    public event Action OnChanged = null!;

    public async Task AddToCartAsync(CartItem cartItem)
    {
        if (await _authService.IsUserAuthenticated())
        {
            await _httpClient.PostAsJsonAsync("api/Cart/add", cartItem);
        }
        else
        {
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();

            var sameItem =
                cart.Find(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);

            if (sameItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }


            await _localStorageService.SetItemAsync("cart", cart);
        }

        await GetCartItemsCount();
    }


    public async Task<List<CartProductResponse>> GetCartProductsAsync()
    {
        if (await _authService.IsUserAuthenticated())
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/Cart");
            return response!.Data!;
        }
        else
        {
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                return new List<CartProductResponse>();
            }

            var response = await _httpClient.PostAsJsonAsync("api/Cart/products", cart);

            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            return cartProducts?.Data!;
        }
    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {

        if (await _authService.IsUserAuthenticated())
        {
            await _httpClient.DeleteAsync($"api/Cart/{productId}/{productTypeId}");
        }
        else
        {
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");


            var cartItem = cart?.Find(x => x.ProductTypeId == productTypeId && x.ProductId == productId);
            if (cartItem == null)
                return;

            cart?.Remove(cartItem);
            await _localStorageService.SetItemAsync("cart", cart);
        }
    
        
    }

    public async Task UpdateQuantity(CartProductResponse product)
    {
        if (await _authService.IsUserAuthenticated())
        {
            var request = new CartItem()
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                ProductTypeId = product.ProductTypeId,
            };
            await _httpClient.PutAsJsonAsync("api/Cart/update-quantity", request);
        }
        else
        {
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
            var cartItem = cart?.Find(x =>
                x.ProductTypeId == product.ProductTypeId && x.ProductId == product.ProductId);
            if (cartItem == null)
                return;

            cartItem.Quantity = product.Quantity;
            await _localStorageService.SetItemAsync("cart", cart);
        }
    }

    public async Task StoreCartItems(bool emptyLocalCart)
    {
        var localCart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
        if (localCart == null)
        {
            return;
        }

        await _httpClient.PostAsJsonAsync("api/Cart", localCart);
        if (emptyLocalCart)
        {
            await _localStorageService.RemoveItemAsync("cart");
        }
    }

    public async Task GetCartItemsCount()
    {
        if (await _authService.IsUserAuthenticated())
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/Cart/count");
            var count = result!.Data;
            await _localStorageService.SetItemAsync("cartItemsCount", count);
        }
        else
        {
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
            await _localStorageService.SetItemAsync("cartItemsCount", cart?.Count ?? 0);
        }

        OnChanged.Invoke();
    }
}