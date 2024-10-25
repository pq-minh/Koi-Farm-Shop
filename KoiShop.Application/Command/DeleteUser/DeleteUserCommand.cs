using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.DeleteUser
{
    public class DeleteUserCommand :IRequest<string>
    {
        public string userId { get; set; }
    }
}
