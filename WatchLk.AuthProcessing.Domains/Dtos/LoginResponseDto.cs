using Microsoft.AspNetCore.Identity;

namespace WatchLk.AuthProcessing.Domains.Dtos
{
    public record LoginResponseDto
    (
        bool Succeeded,
        UserDto? User,
        string? Token,
        List<string>? Errors
    );
}
