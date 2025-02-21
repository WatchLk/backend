using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WatchLk.AuthProcessing.Domains.Models;

namespace WatchLk.AuthProcessing.Application
{
    public class TokenRepository(IConfiguration configuration) : ITokenRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public string CreateJwtToken(User user, string role)
        {
            var signInKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new("userId", user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email!),
                    new(ClaimTypes.Role, role)
                ]),
                Expires = System.DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    signInKey, 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken =  tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);

        }
    }
}
