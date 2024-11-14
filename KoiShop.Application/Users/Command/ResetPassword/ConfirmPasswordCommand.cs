using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.ResetPassword
{
    public class ConfirmPasswordCommand: IRequest<Result>
    {
        public string? Email {  get; set; }

        public string? Token { get; set; }
        public string? Newpassword { get; set; }

        public string? Confirmpassword { get; set; }
    }
}
