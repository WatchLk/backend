﻿using Microsoft.AspNetCore.Identity;
using WatchLk.AuthProcessing.Domains.Dtos;
using WatchLk.AuthProcessing.Domains.Mappings;
using WatchLk.AuthProcessing.Domains.Models;

namespace WatchLk.AuthProcessing.Application
{
    public class AuthRepository(
        UserManager<User> userManager,
        ITokenRepository tokenRepository
        ) : IAuthRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<LoginResponseDto?> Login(LoginRequestDto loginDto)
        {

            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user is null)
                {
                    return new LoginResponseDto(false, null, null, ["User doesn't exist"]);
                }
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!checkPasswordResult)
                {
                    return new LoginResponseDto(false, null, null, ["Invalid password"]);
                }
                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenRepository.CreateJwtToken(user, roles.FirstOrDefault("client"));
                var userDto =  user.toDto(roles.FirstOrDefault("client"));
                return new LoginResponseDto(checkPasswordResult, userDto, token, null);


            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<RegisterResponseDto?> Register(RegisterRequestDto registerDto)
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

                var userExist = await _userManager.FindByEmailAsync(registerDto.Email);

                if (userExist != null)
                {
                    return new RegisterResponseDto(false, ["Email is already taken"]);
                }

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var userRole = await _userManager.AddToRoleAsync(user, registerDto.Role!=null?registerDto.Role.ToLower():"client");

                    if (userRole.Succeeded)
                    {
                        return new RegisterResponseDto(result.Succeeded, []);
                    }

                    return new RegisterResponseDto(userRole.Succeeded, userRole.Errors.Select(e => e.Description).ToList());
                }

                List<string> errors = result.Errors.Select(x => x.Description).ToList();
                return new RegisterResponseDto(result.Succeeded, errors);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
