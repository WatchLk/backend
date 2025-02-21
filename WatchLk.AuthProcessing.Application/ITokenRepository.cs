

using WatchLk.AuthProcessing.Domains.Models;

namespace WatchLk.AuthProcessing.Application
{
    public interface ITokenRepository
    {
        string CreateJwtToken(User user, string role);
    }
}
