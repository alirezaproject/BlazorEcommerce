﻿

namespace BlazorEcommerce.Client.Services.ProductService;

public interface IProductService
{
    event Action ProductChanged;
    List<Product> Products { get; set; }
    string Message { get; set; }
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    string LastSearchText { get; set; }

    Task GetProducts(string categoryUrl = "");
    Task<ServiceResponse<Product>> GetProduct(int productId);

    Task SearchProducts(string searchText,int page);
    Task<List<string>> GetProductSearchSuggestion(string searchText);
}