using AutoMapper;
using KoiShop.Application.AddressDetail.Dtos;
using KoiShop.Application.JwtToken;
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

namespace KoiShop.Application.Users.Queries.GetAllAddress
{
    public class GetAllAddressQueryHandle
        (
        IUserContext userContext,
        IAddressDetailRepository addressDetailRepository,
        IUserStore<User> userStore,
        IMapper mapper
        )
        : IRequestHandler<GetAllAddressQuery, IEnumerable<AddressDetailDtos>>
    {
        public async Task<IEnumerable<AddressDetailDtos>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
            if ( dbUser == null )
            {
                throw new Exception();
            }
            var address = await addressDetailRepository.GetAll(user.Id);
            var addressDtos = mapper.Map<IEnumerable<AddressDetailDtos>>(address);
            return addressDtos;
        }
    }
}
