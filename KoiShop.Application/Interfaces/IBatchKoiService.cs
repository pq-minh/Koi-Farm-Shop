using KoiShop.Application.Dtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IBatchKoiService
    {
        Task<IEnumerable<BatchKoiDto>> GetAllBatchKoi();
        Task<IEnumerable<BatchKoiDto>> GetAllBatchKoiWithCondition(KoiFilterDto koiFilterDto);
    }
}
