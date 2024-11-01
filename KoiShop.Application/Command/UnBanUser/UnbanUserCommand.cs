using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UnBanUser
{
    public class UnbanUserCommand : IRequest<string>
    {
        public string userId { get; set; }
    }
}
