using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchLk.CartService.Domains.Dto;

namespace WatchLk.CartService.Application
{
    public interface ICartRepository
    {
        Task<ResponseDto> AddToCartAsync(CreateCartItemDto createCartItemDto);
        Task<ResponseDto> UpdateCartItemAsync(int cartItemId, UpdateCartItemDto updateCartItemDto);
        Task<ResponseDto> GetCartByUserIdAsync(string userId);
        Task<ResponseDto> RemoveFromCartAsync(int cartItemId);
        Task<ResponseDto> ClearCartAsync(string userId);
    }
}
