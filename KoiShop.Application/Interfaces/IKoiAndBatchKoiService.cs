using KoiShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IKoiAndBatchKoiService
    {
        Task<KoiAndBatchKoiDto> GetAllKoiAndBatch(KoiFilterDto koiFilterDto);
    }
}
