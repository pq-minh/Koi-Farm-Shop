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
            IEmailSender emailSender) : IRequestHandler<ConfirmPasswordCommand, Result>
    {
        public async Task<Result> Handle(ConfirmPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await identityUser.FindByEmailAsync(request.Email);

            if (request.Newpassword != request.Confirmpassword)
            {
                return Result.Failure("Confirm password does not match.");
            }

            var result = await identityUser.ResetPasswordAsync(user, request.Token, request.Newpassword);
            if (result.Succeeded)
            {
                return Result.Success("Reset password success");
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Failure(errors);
        }

    }
}
public class Result
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public IEnumerable<string> Errors { get; private set; }

    public static Result Success(string message)
    {
        return new Result { IsSuccess = true, Message = message };
    }

    public static Result Failure(string message)
    {
        return new Result { IsSuccess = false, Message = message, Errors = new[] { message } };
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result { IsSuccess = false, Errors = errors };
    }
}
