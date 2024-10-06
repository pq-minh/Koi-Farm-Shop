using KoiShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.Logout
{
    internal class LogoutCommandHandle(
            SignInManager<User> signInManager,
            ILogger<LogoutCommand> logger
        ) : IRequestHandler<LogoutCommand, string>
    {
        public async Task<string> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("user logged out");
            return "User logout";
        }
    }
}
