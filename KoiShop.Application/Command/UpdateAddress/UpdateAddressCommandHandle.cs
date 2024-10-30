using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Command.UpdateAddress
{
    public class UpdateAddressCommandHandle(IAddressDetailRepository addressDetailRepository) : IRequestHandler<UpdateAddressCommand, string>
    {
        public async Task<string> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await addressDetailRepository.UpdateAddress(request.Id,request.City,request.District,request.Ward,request.StreetName);
            if (address)
            {
                return "Update Address Successfully";
            }
            return "Fail to update address";
        }
    }
}
