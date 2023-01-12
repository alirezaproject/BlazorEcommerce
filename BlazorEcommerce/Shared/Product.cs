using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerce.Shared;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public bool Featured { get; set; } = false;

    #region Relations

    public Category? Category { get; set; }
    public int CategoryId { get; set; }

    public List<ProductVariant> Variants { get; set; } = new();

    #endregion
}