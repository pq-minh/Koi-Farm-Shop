using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KoiShop.Application.Users.Queries.Login
{
    public class LoginUserQueryHandler(IJwtTokenService jwtTokenService,
        UserManager<User> identityUser,
        RoleManager<IdentityRole> identityRole) : 
        IRequestHandler<LoginUserQuery , string>
    {
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await identityUser.FindByNameAsync(request.Login.Email);
            if (user != null && await identityUser.CheckPasswordAsync(user, request.Login.Password))
            {           
               var token = jwtTokenService.GenerateToken(user);
                return await token;
            }
            throw new UnauthorizedAccessException("Invalid username or password");
        }
    }
}
