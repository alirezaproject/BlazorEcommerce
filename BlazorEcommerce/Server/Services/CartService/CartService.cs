using System.Security.Claims;
using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataBaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(DataBaseContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponse>>()
        {
            Data = new List<CartProductResponse>()
        };

        foreach (var cartItem in cartItems)
        {
            var product = await _context.Products
                .Where(p => p.Id == cartItem.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                continue;
            }

            var productVariant = await _context.ProductVariants
                .Where(v => v.ProductId == cartItem.ProductId
                            && v.ProductTypeId == cartItem.ProductTypeId)
                .Include(v => v.ProductType)
                .FirstOrDefaultAsync();

            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponse()
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType?.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = cartItem.Quantity
            };
            result.Data.Add(cartProduct);
        }

        return result;
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
    {
        cartItems.ForEach(cartItem => cartItem.UserId = GetUserId());
        _context.CartItems.AddRange(cartItems);
        await _context.SaveChangesAsync();
        return await GetDbCartProducts();
    }

    public async Task<ServiceResponse<int>> GetCartItemsCount()
    {
        return new ServiceResponse<int>()
        {
            Data = await _context.CartItems.CountAsync(x => x.UserId == GetUserId())
        };
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts()
    {
        return await GetCartProductsAsync(await _context.CartItems.Where(ci => ci.UserId == GetUserId()).ToListAsync());
    }

    public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
    {
        cartItem.UserId = GetUserId();

        var sameItem = await _context.CartItems
            .FirstOrDefaultAsync(ci =>
            ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId &&
            ci.UserId == cartItem.UserId);

        if (sameItem == null)
        {
            _context.CartItems.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await _context.SaveChangesAsync();
        return new ServiceResponse<bool>() { Data = true };
    }

    public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
    {
        var dbCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci =>
                ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId &&
                ci.UserId == GetUserId());

        if (dbCartItem == null)
        {
            return new ServiceResponse<bool>()
            {
                Data = false,
                Message = "Cart item does not exist .",
                Success = false
            };
        }
        dbCartItem.Quantity = cartItem.Quantity;
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>() { Data = true }; 
    }

    public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
    {
        var dbCartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci =>
                ci.ProductId == productId && ci.ProductTypeId == productTypeId &&
                ci.UserId == GetUserId());

        if (dbCartItem == null)
        {
            return new ServiceResponse<bool>()
            {
                Data = false,
                Message = "Cart item does not exist .",
                Success = false
            };
        }

        _context.CartItems.Remove(dbCartItem);
        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>(){Data = true};
    }

    private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}