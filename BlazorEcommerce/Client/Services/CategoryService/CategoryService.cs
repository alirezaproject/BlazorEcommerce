namespace BlazorEcommerce.Client.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<Category>? Categories { get; set; }

    public async Task GetCategories()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
        if (result?.Data != null)
            Categories = result.Data;

    }
}