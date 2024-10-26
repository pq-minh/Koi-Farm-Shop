using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    public class QuotationService : IQuotationService
    {
        private IQuotationRepository _quotationRepository;
        public QuotationService(IQuotationRepository quotationRepository) 
        {
            _quotationRepository = quotationRepository;
        }


        public async Task<IEnumerable<Quotation>> GetQuotations(string status, DateTime startDate, DateTime endDate)
        {
            return await _quotationRepository.GetQuotations(status, startDate, endDate);
        }


        public async Task<int> GetMostConsignedKoi(DateTime startDate, DateTime endDate)
        {
            Dictionary<int, int> koiDic = new Dictionary<int, int>();

            var packages = await _quotationRepository.GetPackages("Confirmed", startDate, endDate);

            foreach (var item in packages)
            {
                if (item.KoiId.HasValue && item.Quantity.HasValue) // Kiểm tra các thuộc tính hợp lệ
                {
                    if (koiDic.ContainsKey(item.KoiId.Value))
                        koiDic[item.KoiId.Value] += item.Quantity.Value;
                    else
                        koiDic.Add(item.KoiId.Value, item.Quantity.Value);
                }
            }

            int totalQuantity = 0;
            int id = -1;
            foreach (var item in koiDic)
            {
                if (item.Value > totalQuantity)
                {
                    totalQuantity = item.Value;
                    id = item.Key;
                }
            }
            return id;
        }

    }
}
