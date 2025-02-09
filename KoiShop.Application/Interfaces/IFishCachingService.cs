using KoiShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IFishCachingService
    {
        Task<IEnumerable<KoiDto>> GetFishOnSale();

        Task<KoiDto> GetFishIdOnsale(int id);
    }
}
