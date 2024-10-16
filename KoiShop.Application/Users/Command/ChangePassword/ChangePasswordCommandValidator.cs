using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
            public ChangePasswordCommandValidator() { 
            RuleFor(dto => dto.OldPassword).NotEmpty();
            RuleFor(dto => dto.NewPassword)
                   .NotEmpty()
                   .WithMessage("New password is required.")
                   .MinimumLength(6)
                   .WithMessage("New password must be at least 6 characters long.")
                   .Matches(@"[A-Z]")
                   .WithMessage("New password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]")
                   .WithMessage("New password must contain at least one lowercase letter.")
                   .Matches(@"[!@#$%^&*(),.?""{}|<>]")
                   .WithMessage("New password must contain at least one special character.");
            RuleFor(dto => dto.ConfirmPassword)
             .NotEmpty()
             .WithMessage("Confirm password is required.")
             .Equal(dto => dto.NewPassword)
             .WithMessage("Confirm password must match the new password.");

        }
    }
}
