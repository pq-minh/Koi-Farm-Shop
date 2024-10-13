using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace KoiShop.Application.JwtToken
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(User identityUser);
    }
}
