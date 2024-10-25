    using KoiShop.Application.JwtToken;
    using KoiShop.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using static KoiShop.Application.Users.UserContext;

    namespace KoiShop.Application.Users.Command.ResetPassword
    {
        public class ResetPasswordCommandHandle(IUserContext userContext,
            IUserStore<User> userStore,
            IJwtTokenService jwtTokenService,
            UserManager<User> identityUser,
            IEmailSender emailSender) : IRequestHandler<ResetPasswordCommand, string>
        {
            public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await identityUser.FindByEmailAsync(request.email);
                var token = await identityUser.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = $"https://localhost:5173/Account/ResetPassword?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";
                var subject = "Reset Password";
                var plainTextContent = "Please reset your password.";
               var htmlContent = $"<strong>Please reset your password by clicking here: <a href='{callbackUrl}'>link</a></strong>";
               await emailSender.SendEmailAsync(request.email,subject,plainTextContent,htmlContent);
            return "Email reset password đã được gửi!";

        }
    }
    }
