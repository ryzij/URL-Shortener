using URL_Shortener.Models;
using System.IdentityModel.Tokens.Jwt;
using URL_Shortener.Settings;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace URL_Shortener.Services
{
    public class JwtService(IOptions<AuthSettings> options)
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>() {
                new("userName", user.Name),
                new("userEmail", user.Email),
                new("id", user.Id.ToString())
            };
            var jwt = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
