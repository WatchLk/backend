using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchLk.AuthProcessing.Application;
using WatchLk.AuthProcessing.Domains.Dtos;

namespace WatchLk.AuthProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        private readonly IAuthRepository _authRepository = authRepository;


        //[HttpPost(Name = "Login")]
        //public async Task<IResult> Login([FromBody] LoginDto login)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Results.BadRequest("All fields are required");
        //    }
        //    return Results.Accepted();
        //}

        [HttpPost(Name = "Register")]
        public async Task<IResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest("All fields are required");
            }

            var result = await _authRepository.Register(registerDto);
            if(result is null)
            {
                return Results.BadRequest("Something went wrong");
            }

            if (result.Succeeded)
            {
                return Results.Ok(result);
            }

            return Results.BadRequest(result);
        }
    }
}
