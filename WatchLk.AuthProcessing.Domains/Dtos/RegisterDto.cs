
namespace WatchLk.AuthProcessing.Domains.Dtos
{
    public record RegisterDto
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
