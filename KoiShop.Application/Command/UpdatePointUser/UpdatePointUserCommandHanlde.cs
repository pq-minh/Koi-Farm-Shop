using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdatePointUser
{
    public class UpdatePointUserCommandHanlde(
            IUserRepository userRepository
        ) : IRequestHandler<UpdatePointUserCommand, string>
    {
        public async Task<string> Handle(UpdatePointUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateResult = await userRepository.UpdateUser(request.UserID, request.FirstName, request.LastName, request.Point);
                if (updateResult != null)
                {
                    return "User updated successfully.";
                }

                return "User update failed.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
