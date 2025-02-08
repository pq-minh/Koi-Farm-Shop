using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Queries.GetAllKoi
{
    public class GetAllKoiQueryHandle(
         IFishRepository koiRepository   
        ) : IRequestHandler<GetAllKoiQuery, IEnumerable<Koi>>
    {
        public async Task<IEnumerable<Koi>> Handle(GetAllKoiQuery request, CancellationToken cancellationToken)
        {
           var koi = await koiRepository.GetAllFish<Koi>();
            return koi;
        }
    }
}
