

namespace WatchLk.CartService.Application
{
    public interface ITokenRepository
    {
        Task<bool> IsUserAuthorized(string token, string userId);
    }
}
