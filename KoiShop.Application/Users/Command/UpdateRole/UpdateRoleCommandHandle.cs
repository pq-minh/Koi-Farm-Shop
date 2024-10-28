using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.UpdateRole
{
    public class UpdateRoleCommandHandle(IJwtTokenService jwtTokenService,
        UserManager<User> identityUser,
        RoleManager<IdentityRole> identityRole) : IRequestHandler<UpdateRoleCommand, string>
    {
        public async Task<string> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await identityUser.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return "Email isn't exists.";
            }
            var roleExists = await identityRole.RoleExistsAsync(request.RoleName);
            if (!roleExists)
            {
                return "Role isn't exists.";
            }
            var currentRoles = await identityUser.GetRolesAsync(user);
            await identityUser.RemoveFromRolesAsync(user, currentRoles);
            await identityUser.AddToRoleAsync(user, request.RoleName);
            return $"User {request.Email} role updated to {request.RoleName}.";
        }
    }
}
