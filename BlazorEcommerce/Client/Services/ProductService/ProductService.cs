using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public event Action ProductChanged = null!;
    public List<Product> Products { get; set; } = new();
    public List<Product> AdminProduct { get; set; } = new();
    public string Message { get; set; } = "Loading Products ...";
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public string LastSearchText { get; set; } = string.Empty;

    public async Task GetProducts(string categoryUrl = "")
    {
        var result = string.IsNullOrWhiteSpace(categoryUrl)
                ? await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/featured")
                : await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(
                    $"api/Product/category/{categoryUrl}")
            ;

        if (result?.Data != null)
        {
            Products = result.Data;
        }

        CurrentPage = 1;
        PageCount = 0;

        if (Products.Count == 0)
        {
            Message = "No Products Found";
        }


        ProductChanged.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return result!;
    }

    public async Task SearchProducts(string searchText, int page)
    {
        LastSearchText = searchText;
        var result =
            await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/Product/search/{searchText}/{page}");

        if (result is { Data: { } })
        {
            Products = result.Data.Products;
            CurrentPage = result.Data.CurrentPage;
            PageCount = result.Data.Pages;
        }

        if (Products.Count == 0)
        {
            Message = "No Products Found .";
        }
        ProductChanged.Invoke();

    }

    public async Task<List<string>> GetProductSearchSuggestion(string searchText)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/searchSuggestions/{searchText}");

        return result?.Data!;
    }

    public async Task GetAdminProducts()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/admin");

        AdminProduct = result?.Data!;
        CurrentPage = 1;
        PageCount = 0;
        if (AdminProduct.Count == 0)
            Message = "No Products Found";

    }

    public async Task<Product> CreateProduct(Product product)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Product", product);
        var newProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>())?.Data!;

        return newProduct;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var result = await _httpClient.PutAsJsonAsync("api/Product", product);
        return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>())?.Data!;
    }

    public async Task DeleteProduct(Product product)
    {
        var result = await _httpClient.DeleteAsync($"api/Product/{product.Id}");
    }
}