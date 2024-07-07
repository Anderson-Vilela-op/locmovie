using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace locmovie.Utils
{
    public class TokenUtils : ITokenUtils
    {
        private readonly string _secretKey;

        public TokenUtils(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "locmovies.com",
                audience: "locmovies.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}