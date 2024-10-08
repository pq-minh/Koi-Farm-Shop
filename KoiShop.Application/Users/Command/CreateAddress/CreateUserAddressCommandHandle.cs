using KoiShop.Application.JwtToken;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Users.Command.UpdateAddress
{
    public class CreateUserAddressCommandHandle(
        IUserContext userContext,
        IJwtTokenService jwtTokenService,
        UserManager<User> identityUser,
        IAddressDetailRepository addressDetailRepository,
        IUserStore<User> userStore) : IRequestHandler<CreateUserAddressCommand, int>
    {
        public async Task<int> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var newAddress = new AddressDetail
            {
                City = request.City,
                Dictrict = request.Dictrict,
                UserId =user.Id,
                StreetName = request.StreetName,
                Ward = request.Ward,
                User = dbUser // Gán đối tượng người dùng
            };
            int id = await addressDetailRepository.Create(newAddress);
            return id;
            throw new NotImplementedException();
        }
    }
}
