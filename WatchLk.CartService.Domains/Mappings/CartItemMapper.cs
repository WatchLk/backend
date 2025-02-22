using WatchLk.CartService.Domains.Dto;
using WatchLk.CartService.Domains.Model;

namespace WatchLk.CartService.Domains.Mappings
{
    public static class CartItemMapper
    {
        public static CartItemDto ToDto(this CartItem cartItem)
        {
            return new CartItemDto(
                cartItem.Id, 
                cartItem.Name,
                cartItem.UserId,
                cartItem.Brand, 
                cartItem.Category, 
                cartItem.ImageUrl, 
                cartItem.UnitPrice, 
                cartItem.Quantiy, 
                cartItem.CreatedAt, 
                cartItem.UpdatedAt
            );
        }

        public static CartItem ToEntity(this CreateCartItemDto createCartItem)
        {
            return new CartItem
            {
                Name = createCartItem.Name,
                UserId = createCartItem.UserId,
                Brand = createCartItem.Brand,
                Category = createCartItem.Category,
                ImageUrl = createCartItem.ImageUrl,
                UnitPrice = createCartItem.UnitPrice,
                Quantiy = createCartItem.Quantity
            };
        }

        public static CartItem ToEntity(this UpdateCartItemDto updateCartItem, int Id)
        {
            return new CartItem
            {
                Id = Id,
                Name = updateCartItem.Name,
                UserId = updateCartItem.UserId,
                Brand = updateCartItem.Brand,
                Category = updateCartItem.Category,
                ImageUrl = updateCartItem.ImageUrl,
                UnitPrice = updateCartItem.UnitPrice,
                Quantiy = updateCartItem.Quantity
            };
        }
    }
}
