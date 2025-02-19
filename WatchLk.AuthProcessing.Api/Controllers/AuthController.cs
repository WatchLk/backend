﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WatchLk.AuthProcessing.Api.Helpers;
using WatchLk.AuthProcessing.Application;
using WatchLk.AuthProcessing.Domains.Dtos;

namespace WatchLk.AuthProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        private readonly IAuthRepository _authRepository = authRepository;


        [HttpPost]
        [Route("login")]
        public async Task<IResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest("All fields are required");
            }
            try
            {
                var result = await _authRepository.Login(loginDto);
                if (result is null)
                {
                    return Results.BadRequest(new LoginResponseDto(false, loginDto.Email, null, null, ["Something went wrong"]));
                }

                if (!result.Succeeded)
                {
                    return Results.BadRequest(result);
                }

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new LoginResponseDto(false, loginDto.Email, null, null, [ex.Message]));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest("All fields are required");
            }

            if (!Validators.IsValidEmail(registerDto.Email))
            {
                return Results.BadRequest(new RegisterResponseDto(false, ["Email is not valid"]));
            }

            var result = await _authRepository.Register(registerDto);

            if(result is null)
            {
                return Results.BadRequest(new RegisterResponseDto(false, ["Something went wrong"]));
            }

            if (result.Succeeded)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest(result);
        }

        
    }
}
