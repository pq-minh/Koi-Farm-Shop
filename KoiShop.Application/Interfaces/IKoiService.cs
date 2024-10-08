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
        Task<IEnumerable<Koi>> GetAllKoi();
        Task<IEnumerable<Koi>> GetAllKoiWithCondition(string koiName, double? from, double? to, string sortBy, int pageNumber, int pageSize);

    }
}
