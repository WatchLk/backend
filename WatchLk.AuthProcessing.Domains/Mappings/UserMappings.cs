using WatchLk.AuthProcessing.Domains.Dtos;
using WatchLk.AuthProcessing.Domains.Models;

namespace WatchLk.AuthProcessing.Domains.Mappings
{
    public static class UserMappings
    {
        public static UserDto toDto(this User user, string role) =>
            new(user.Id.ToString(), user.FirstName, user.LastName, user.Email!, role);
    }
}
