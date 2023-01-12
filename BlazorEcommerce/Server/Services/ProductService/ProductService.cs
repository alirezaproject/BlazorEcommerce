using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataBaseContext _context;

    public ProductService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        return new ServiceResponse<List<Product>>()
        {
            Data = await _context.Products
                .Include(p => p.Variants)
                .ToListAsync()
        };
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var operation = new ServiceResponse<Product>();

        var product = await _context.Products
            .Include(p => p.Variants)
            .ThenInclude(v => v.ProductType)
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            operation.Success = false;
            operation.Message = "Sorry, But this product does not exist.";
        }
        else
        {
            operation.Data = product;
        }

        return operation;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categotyUrl)
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _context.Products.Where(x => x.Category!.Url.ToLower().Equals(categotyUrl))
                .Include(p => p.Variants)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page)
    {
        const float pageResult = 2f;
        var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResult);

        var products = await _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                                          p.Description.ToLower().Contains(searchText.ToLower()))
            .Include(p => p.Variants)
            .Skip((page - 1) * (int)pageResult)
            .Take((int)pageResult)
            .ToListAsync();

        var response = new ServiceResponse<ProductSearchResult>()
        {
            Data = new ProductSearchResult()
            {
                Products = products,
                CurrentPage = page,
                Pages = (int)pageCount
            }
        };

        return response;
    }

    private async Task<List<Product>> FindProductsBySearchText(string searchText)
    {
        return await _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                                  p.Description.ToLower().Contains(searchText.ToLower()))
            .Include(p => p.Variants)
            .ToListAsync();
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
    {
        var products = await FindProductsBySearchText(searchText);

        var result = new List<string>();

        foreach (var product in products)
        {
            if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(product.Title);
            }

            if (product.Description != null)
            {
                var punctuation = product.Description.Where(char.IsPunctuation)
                    .Distinct().ToArray();
                var words = product.Description.Split()
                    .Select(s => s.Trim(punctuation));

                foreach (var word in words)
                {
                    if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                    {
                        result.Add(word);
                    }
                }
            }
        }

        return new ServiceResponse<List<string>>() { Data = result };
    }

    public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _context.Products.Where(p => p.Featured)
                .Include(p => p.Variants)
                .ToListAsync()
        };
        return response;
    }
}