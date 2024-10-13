using KoiShop.Application.JwtToken;
using KoiShop.Domain.Constant;
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
                FirstName = request.RegisterRQ.FirstName,
                LastName = request.RegisterRQ.LastName,
                Email = request.RegisterRQ.Email,
                PhoneNumber = request.RegisterRQ.PhoneNumber,
                UserName = request.RegisterRQ.Email,
                Point = 0,
                Status = "IsActived"
            };
           
            if (request.RegisterRQ.ConfirmPassword != request.RegisterRQ.Password)
            {
                throw new ArgumentException("Passwords do not match.");
            }
           var result = await identityUser.CreateAsync(user, request.RegisterRQ.Password);
            
            if (result.Succeeded)
            {
                await identityUser.AddToRoleAsync(user,UserRoles.Customer);
                return true; 
            }
            throw new InvalidOperationException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
