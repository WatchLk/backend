using Microsoft.EntityFrameworkCore;
using WatchLk.CartService.Domains.Dto;
using WatchLk.CartService.Domains.Mappings;
using WatchLk.CartService.Infrastructure;

namespace WatchLk.CartService.Application
{
    public class CartRepository(AppDbContext dbContext) : ICartRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        public async Task<ResponseDto> AddToCartAsync(CreateCartItemDto createCartItemDto)
        {
            try
            {
                var existingItem = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Name == createCartItemDto.Name && c.UserId == createCartItemDto.UserId);
                if (existingItem != null)
                {
                    return new ResponseDto(false, "Item already in the cart", null);
                }
                var item = createCartItemDto.ToEntity();
                item.CreatedAt = DateTime.UtcNow;
                item.UpdatedAt = DateTime.UtcNow;
                await _dbContext.CartItems.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto(true, "Item added to cart", null);
            } catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message, null);
            }
        }

        public async Task<ResponseDto> UpdateCartItemAsync(int cartItemId, UpdateCartItemDto updateCartItemDto)
        {
            try
            {
                var existingItem = await _dbContext.CartItems.FindAsync(cartItemId);
                if (existingItem is null)
                {
                    return new ResponseDto(false, "Item not found", null);
                }
                var newItem = updateCartItemDto.ToEntity(cartItemId);
                newItem.UpdatedAt = DateTime.UtcNow;
                _dbContext.Entry(existingItem)
                    .CurrentValues
                    .SetValues(newItem);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto(true, "Item updated successfully", [newItem.ToDto()]);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message, null);
            }
        }

        public async Task<ResponseDto> ClearCartAsync(string userId)
        {
            try
            {
                await _dbContext.CartItems.Where(c => c.UserId == userId)
                                          .ExecuteDeleteAsync();
                return new ResponseDto(true, "Cart cleared successfully", null);

            } catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message, null);
            }
        }

        public async Task<ResponseDto> GetCartByUserIdAsync(string userId)
        {
            try
            {
                var cartItems = await _dbContext.CartItems.Where(c => c.UserId == userId)
                                                          .Select(c => c.ToDto())
                                                          .ToListAsync();
                return new ResponseDto(true, "Cart fetched", cartItems);

            } catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message, null);
            }
        }

        public async Task<ResponseDto> RemoveFromCartAsync(int cartItemId)
        {
            try
            {
                await _dbContext.CartItems.Where(c => c.Id == cartItemId)
                .ExecuteDeleteAsync();
                return new ResponseDto(true, "Item removed from cart", null);

            } catch(Exception ex)
            {
                return new ResponseDto(false, ex.Message, null);
            }
        }
    }
}
