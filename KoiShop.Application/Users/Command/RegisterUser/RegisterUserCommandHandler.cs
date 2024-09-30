using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.RegisterUser
{
    public class RegisterUserCommandHandler(IJwtTokenService jwtTokenService,
        UserManager<User> identityUser,
        RoleManager<IdentityRole> identityRole) : IRequestHandler<RegisterUserCommand, bool>

    {
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.RegisterRQ.Email,
                UserName = request.RegisterRQ.Email
            };
            if (request.RegisterRQ.ConfirmPassword != request.RegisterRQ.Password)
            {
                throw new ArgumentException("Passwords do not match.");
            }
           var result = await identityUser.CreateAsync(user, request.RegisterRQ.Password);
            if (result.Succeeded)
            {
                return true; 
            }
            throw new InvalidOperationException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
