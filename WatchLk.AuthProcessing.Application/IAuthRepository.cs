
using Microsoft.AspNetCore.Identity;
using WatchLk.AuthProcessing.Domains.Dtos;

namespace WatchLk.AuthProcessing.Application
{
    public interface IAuthRepository
    {
        Task<ResponseDto?> Login(LoginDto loginDto);
        Task<ResponseDto?> Register(RegisterDto registerDto);
    }
}
