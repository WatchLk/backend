using Microsoft.AspNetCore.Identity;

namespace WatchLk.AuthProcessing.Domains.Dtos
{
    public record LoginResponseDto
    (
        bool Succeeded,
        string? Email,
        string? Token,
        string? Role,
        List<string>? Errors
    );
}
