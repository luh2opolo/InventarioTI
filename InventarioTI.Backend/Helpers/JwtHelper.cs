using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioTI.Helpers
{
    // Clase encargada de generar tokens JWT
    public class JwtHelper
    {
        private readonly string _key;

        public JwtHelper(string key)
        {
            _key = key;
        }

        // Generar token JWT
        public string GenerateToken(int userId, string fullName, string role)
        {
            var claims = new[]
            {
                new Claim("userId", userId.ToString()),
                new Claim("fullName", fullName),
                new Claim(ClaimTypes.Role, role)
            };

            var keyBytes = Encoding.UTF8.GetBytes(_key);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}