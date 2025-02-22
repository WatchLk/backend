

namespace WatchLk.CartService.Domains.Dto
{
    public record CartItemDto
    (
        int Id,
        string Name,
        string UserId,
        string Brand,
        string Category,
        string ImageUrl,
        double UnitPrice,
        int Quantity,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
