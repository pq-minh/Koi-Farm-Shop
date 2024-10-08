using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Users.Command.UpdateUser
{
    public class UpdateUserCommandHandler(IUserContext userContext,
        IUserStore<User> userStore,
        IJwtTokenService jwtTokenService,
        UserManager<User> identityUser) : IRequestHandler<UpdateUserCommands, string>
    {
        //public async Task Handle(UpdateUserCommands request, CancellationToken cancellationToken)
        //{
        //    var user = userContext.GetCurrentUser();
        //    var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
        //    if (dbUser == null)
        //    {
        //        throw new DirectoryNotFoundException(nameof(User));
        //    }
        //    dbUser.FirstName = request.FirstName;
        //    dbUser.LastName = request.LastName;
        //    dbUser.Email = request.Email;
        //    dbUser.PhoneNumber = request.PhoneNumber;
        //    await userStore.UpdateAsync(dbUser, cancellationToken);
        //    var token = jwtTokenService.GenerateToken(user);
        //    return await token;
        //}
        public async Task<string> Handle(UpdateUserCommands request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
            if (dbUser == null)
            {
                throw new DirectoryNotFoundException(nameof(User));
            }
            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;
            dbUser.Email = request.Email;
            dbUser.UserName = request.Email;
            dbUser.PhoneNumber = request.PhoneNumber;
            await userStore.UpdateAsync(dbUser, cancellationToken);
            var token = jwtTokenService.GenerateToken(dbUser);
            return await token;
        }
    }
}
