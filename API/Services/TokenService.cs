using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Entities;
using API.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        // ctor
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["tokenKey"]));
        }
        public string CrearToken(AppUser user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var credenciales = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(7),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

// Singleton C# Gang of four
