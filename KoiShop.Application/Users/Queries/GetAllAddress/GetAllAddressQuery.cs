using KoiShop.Application.AddressDetail.Dtos;
using KoiShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users.Queries.GetAllAddress
{
    public class GetAllAddressQuery : IRequest<IEnumerable<AddressDetailDtos>>
    {

    }
}
