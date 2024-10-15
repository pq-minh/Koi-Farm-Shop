using KoiShop.Application.Dtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IKoiService
    {
        Task<IEnumerable<KoiDto>> GetAllKoi();
        Task<KoiDto> GetKoi(int id);
        Task<IEnumerable<KoiDto>> GetAllKoiWithCondition(KoiFilterDto koiFilterDto);

    }
}
