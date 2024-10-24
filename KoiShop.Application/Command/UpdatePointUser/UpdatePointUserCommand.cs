using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdatePointUser
{
    public class UpdatePointUserCommand : IRequest<string>
    {
        public string? UserID { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int Point { get; set; }
    }
}
