#pragma warning disable CS8618
namespace BlazorEcommerce.Client.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public event Action OnChanged;
    public List<Category>? Categories { get; set; }
    public List<Category>? AdminCategories { get; set; }


    public async Task GetCategories()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
        if (result?.Data != null)
            Categories = result.Data;

    }

    public async Task GetAdminCategories()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category/admin");
        if (result?.Data != null)
            AdminCategories = result.Data;
    }

    public async Task AddCategory(Category category)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Category/admin", category);
        AdminCategories = ((await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())!).Data;
        await GetCategories();
        OnChanged.Invoke();
    }

    public async Task UpdateCategory(Category category)
    {
        var response = await _httpClient.PutAsJsonAsync("api/Category/admin", category);
        AdminCategories = ((await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())!).Data;
        await GetCategories();
        OnChanged.Invoke();
    }

    public async Task DeleteCategory(int categoryId)
    {
        var response = await _httpClient.DeleteAsync($"api/Category/admin/{categoryId}");
        AdminCategories = ((await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>())!).Data;
        await GetCategories();
        OnChanged.Invoke();
    }

    public Category CreateNewCategory()
    {
        var newCategory = new Category() { IsNew = true, Editing = true };
        AdminCategories?.Add(newCategory);
        OnChanged.Invoke();
        return newCategory;
    }
}