using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IQuotationService
    {
        Task<IEnumerable<Quotation>> GetQuotations(string status, DateTime startDate, DateTime endDate);
        Task<int> GetMostConsignedKoi(DateTime startDate, DateTime endDate);
    }
}
