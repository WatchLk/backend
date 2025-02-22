

using System.ComponentModel.DataAnnotations;

namespace WatchLk.CartService.Domains.Dto
{
    public record CreateCartItemDto
    (
        [Required]
        string Name,
        [Required]
        string UserId,
        [Required]
        string Brand,
        [Required]
        string Category,
        [Required]
        string ImageUrl,
        [Required]
        [Range(0,Double.MaxValue)]
        double UnitPrice,
        [Range(1, Double.MaxValue)]
        int Quantity
    );
}
