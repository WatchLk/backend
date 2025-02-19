
using Microsoft.AspNetCore.Identity;
using WatchLk.AuthProcessing.Domains.Dtos;

namespace WatchLk.AuthProcessing.Application
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto?> Login(LoginRequestDto loginDto);
        Task<RegisterResponseDto?> Register(RegisterRequestDto registerDto);
    }
}
