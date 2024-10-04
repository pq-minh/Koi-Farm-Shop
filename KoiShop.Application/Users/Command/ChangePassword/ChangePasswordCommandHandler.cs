using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Users.Command.ChangePassword
{
    public class ChangePasswordCommandHandler(IUserContext userContext,
        IUserStore<User> userStore,
        IJwtTokenService jwtTokenService,
        UserManager<User> identityUser,
        SignInManager<User> signInManager
        ) : IRequestHandler<ChangePasswordCommand, string>
    {
        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var dbUser = await userStore.FindByIdAsync(user!.Id,cancellationToken);
            if (dbUser == null)
            {
                throw new Exception(nameof(user));
            }
            var result = await identityUser.ChangePasswordAsync(dbUser, request.OldPassword, request.NewPassword);
            if (result.Succeeded)
            {
                await signInManager.SignOutAsync();
                return "ChangePasswordConfirm";
            }
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Password change failed: {errors}");
        }
    }
}
