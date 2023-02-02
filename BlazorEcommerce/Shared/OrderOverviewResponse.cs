namespace BlazorEcommerce.Shared;

public class OrderOverviewResponse
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Product { get; set; } = string.Empty;
    public string ProductImageUrl { get; set; } = string.Empty;

}