using Microsoft.AspNetCore.Identity;

namespace WatchLk.AuthProcessing.Domains.Dtos
{
    public record RegisterResponseDto
    (
        bool Succeeded,
        List<string>? Errors
    );
}
