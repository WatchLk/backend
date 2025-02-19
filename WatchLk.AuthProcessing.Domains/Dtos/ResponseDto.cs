using Microsoft.AspNetCore.Identity;

namespace WatchLk.AuthProcessing.Domains.Dtos
{
    public record ResponseDto
    (
        bool Succeeded,
        IdentityResult? Result,
        IEnumerable<IdentityError>? Error
    );
}
