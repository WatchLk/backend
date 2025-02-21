namespace WatchLk.AuthProcessing.Domains.Dtos
{
    public record UserDto
    (
        string UserId,
        string FirstName,
        string LastName,
        string Email,
        string Role
    );
}
