using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Command.RegisterUser
{
    public class RegisterUserCommand(RegisterRQ registerRQ) : IRequest<bool>
    {
        public RegisterRQ RegisterRQ = registerRQ;
    }
}
