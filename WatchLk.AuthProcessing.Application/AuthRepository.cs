using Microsoft.AspNetCore.Identity;
using WatchLk.AuthProcessing.Domains.Dtos;
using WatchLk.AuthProcessing.Domains.Models;

namespace WatchLk.AuthProcessing.Application
{
    public class AuthRepository(UserManager<User> userManager) : IAuthRepository
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<ResponseDto?> Login(LoginDto loginDto)
        {
            var user  = await _userManager.FindByEmailAsync(loginDto.Email);

            return new ResponseDto(true,null,null);
        }

        public async Task<ResponseDto?> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    return new ResponseDto(result.Succeeded, result, null);
                }

                return new ResponseDto(result.Succeeded, result, result.Errors);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
