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
    class BatchKoiService : IBatchKoiService
    {
        private readonly IBatchKoiRepository _repository;
        public BatchKoiService(IBatchKoiRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BatchKoi>> GetAllBatchKoi()
        {
            return await _repository.GetAllBatch();
        }
    }
}
