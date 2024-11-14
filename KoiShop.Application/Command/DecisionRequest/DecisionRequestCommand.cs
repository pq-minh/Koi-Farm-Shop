using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.DecisionRequest
{
    public class DecisionRequestCommand(int requestid,string decision) : IRequest<string>
    {
        public int requestid { get; set; } = requestid;
        public string decision { get; set; } = decision;
    }
}
