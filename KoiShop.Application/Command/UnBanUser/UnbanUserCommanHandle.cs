using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UnBanUser
{
    public class UnbanUserCommanHandle(
        IUserRepository userRepository) : IRequestHandler<UnbanUserCommand, string>
    {
        public async Task<string> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.UnbanUser(request.userId);
            if (user != null)
            {
                return "Unban user complete";
            }
            return "Error when unban user";
        }
    }
}
