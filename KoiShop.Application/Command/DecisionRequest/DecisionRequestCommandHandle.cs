using AutoMapper;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Command.DecisionRequest
{
    public class DecisionRequestCommandHandle(IUserContext userContext,
        IUserStore<User> userStore,
        IRequestRepository requestRepository,
        IMapper mapper) : IRequestHandler<DecisionRequestCommand, string>
    {
        public async Task<string> Handle(DecisionRequestCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            if (user == null)
            {
                return null;
            }
            var result = await requestRepository.DecisionRequest(request.requestid,request.decision);
            return result;
        }
    }
}
