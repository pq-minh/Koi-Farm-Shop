﻿using KoiShop.Domain.Entities;
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
        new Claim("FirstName",user.FirstName),
        new Claim("LastName",user.LastName),
        new Claim("Point",(user.Point ?? 0).ToString()),
        new Claim("PhoneNumber",user.PhoneNumber ?? string.Empty),
        new Claim("Email",user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier cho token
        };
            // Thêm roles vào claims
            claims.AddRange(roles.Select(role => new Claim("role", role ?? string.Empty)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateTokenClaims(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            // Trả về token dưới dạng Task<string>
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
