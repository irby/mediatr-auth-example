using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dappa.Core.Models.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Dappa.Api.Managers;

public class CookieManager
{
    internal static string SigningKey => Environment.GetEnvironmentVariable("JWT_SIGNING_KEY") ?? string.Empty;
    public void SetCookies(HttpResponse response, UserDto userDto)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            expires: DateTime.Now.AddMinutes(30),
            claims: new [] { new Claim(ClaimTypes.Name, userDto.Id.ToString()) },
            signingCredentials: credentials
        );
        var cookie =  new JwtSecurityTokenHandler().WriteToken(token);
        response.Cookies.Append("X-Access-Token", cookie);
    }
}
