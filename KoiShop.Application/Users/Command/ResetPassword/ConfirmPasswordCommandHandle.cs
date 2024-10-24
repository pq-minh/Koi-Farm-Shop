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

namespace KoiShop.Application.Users.Command.ResetPassword
{
    public class ConfirmPasswordCommandHandle(IUserContext userContext,
            IUserStore<User> userStore,
            IJwtTokenService jwtTokenService,
            UserManager<User> identityUser,
            IEmailSender emailSender) : IRequestHandler<ConfirmPasswordCommand, string>
    {
        public async Task<string> Handle(ConfirmPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await identityUser.FindByEmailAsync(request.Email);
            if (user == null) {
                return "Nguoi dung khong ton tai";
            }
            var result = await identityUser.ResetPasswordAsync(user, request.Token, request.Newpassword);
            if (result.Succeeded)
            {
                return "Mật khẩu đã được cập nhật thành công!";
            }
            return "Ok";
        }
    }
}
