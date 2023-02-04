using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.AddressService;

public class AddressService : IAddressService
{
    private readonly HttpClient _httpClient;

    public AddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Address> GetAddress()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Address>>("api/Address");
        return response?.Data!;
    }

    public async Task<Address> AddOrUpdateAddress(Address address)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Address", address);
        return response.Content.ReadFromJsonAsync<ServiceResponse<Address>>().Result?.Data!;
    }
}