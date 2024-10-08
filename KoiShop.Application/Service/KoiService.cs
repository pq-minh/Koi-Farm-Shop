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
    public class KoiService : IKoiService
    {
        private readonly IKoiRepository _koiRepository;
        public KoiService(IKoiRepository koiRepository)
        {
            _koiRepository = koiRepository;
        }
        public async Task<IEnumerable<Koi>> GetAllKoi()
        {
            return await _koiRepository.GetAllKois();
        }

        public async Task<IEnumerable<Koi>> GetAllKoiWithCondition(string koiName, double? from, double? to, string sortBy, int pageNumber, int pageSize)
        {
            return await _koiRepository.GetKoiWithCondition(koiName, from, to, sortBy, pageNumber, pageSize);
        }
    }
}
