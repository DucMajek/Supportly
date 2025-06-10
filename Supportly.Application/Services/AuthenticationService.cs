using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Supportly.Infrastructure.Models;

namespace Supportly.Application.Services
{
    public class AuthenticationService
    {
        public static IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateJwtToken(string userName, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = _configuration["JWT:JwtIssuer"],
                Audience = _configuration["JWT:JwtAudience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:JwtKey"])),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public static List<string> ValidateToken(string token)
        {
            List<string> claims = new List<string>();
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:JwtKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // true, if production 
                    ValidateAudience = false, // true, if production
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var jku = jwtToken.Claims.FirstOrDefault(c => c.Type == "Kid").Value;
                var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "Sub")?.Value;
                var userRole = jwtToken.Claims.First(claim =>
                    claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

                claims.Add(userRole);
                claims.Add(userName);

                return claims;
            }
            catch
            {
                return null;
            }
        }
    }
}