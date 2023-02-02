using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigationManager;

    public OrderService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
        NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _navigationManager = navigationManager;
    }


    public async Task PlaceOrder()
    {
        if (await IsUserAuthenticated())
        {
            await _httpClient.PostAsync("api/Order", null);
        }
        else
        {
            _navigationManager.NavigateTo("login");
        }
    }

    public async Task<List<OrderOverviewResponse>> GetOrders()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/Order");

        return result!.Data!;
    }

    public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/Order/{orderId}");
        return result!.Data!;
    }

    private async Task<bool> IsUserAuthenticated()
    {
        return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }
}