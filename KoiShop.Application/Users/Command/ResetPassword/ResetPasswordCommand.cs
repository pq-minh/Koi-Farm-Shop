using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.ResetPassword
{
    public class ResetPasswordCommand(string email) : IRequest<string>
    {
        public string email { get; set; } = email;
    }
}
