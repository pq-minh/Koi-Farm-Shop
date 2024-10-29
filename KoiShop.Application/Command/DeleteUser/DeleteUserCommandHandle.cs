using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.DeleteUser
{
    public class DeleteUserCommandHandle(
        IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, string>
    {
        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.DeleteUser(request.userId,request.Reason);
            if ( user != null)
            {
                return "Delete user complete";
            }
            return "Error when delete user";
        }
    }
}
