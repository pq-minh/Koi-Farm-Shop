using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.JwtToken
{
    //Setting JWTtoken
    public class JwtTokenService(IConfiguration _configuration, UserManager<User> userManager) : IJwtTokenService
    {
        public async Task<string> GenerateToken(User user)
        {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var roles = await userManager.GetRolesAsync(user);
                var claims = new List<Claim>
         {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id), // ID người dùng
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier cho token
        };
            // Thêm roles vào claims
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
