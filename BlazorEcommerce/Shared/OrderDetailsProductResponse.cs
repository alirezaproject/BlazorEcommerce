namespace BlazorEcommerce.Shared;

public class OrderDetailsProductResponse
{
    public int ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    
}