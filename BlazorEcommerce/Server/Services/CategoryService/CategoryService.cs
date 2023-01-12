namespace BlazorEcommerce.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataBaseContext _context;

    public CategoryService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return new ServiceResponse<List<Category>>()
        {
            Data = categories
        };
    }
}