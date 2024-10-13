using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiShop.Domain.Respositories;
using KoiShop.Application.Interfaces;

namespace KoiShop.Application.Service
{
    public class BatchKoiService : IBatchKoiService
    {
        // abc
        private readonly IBatchKoiRepository _batchKoiRepository;

        public BatchKoiService(IBatchKoiRepository batchKoiRepository)
        {
            _batchKoiRepository = batchKoiRepository;
        }

        public async Task<IEnumerable<BatchKoi>> GetAllBatchKoi()
        {
            return await _batchKoiRepository.GetAllBatchKoiAsync();
        }

        public async Task<BatchKoi> AddBatchKoi(BatchKoi batchKoi)
        {
            return await _batchKoiRepository.AddBatchKoiAsync(batchKoi);
        }

        public async Task<BatchKoi> UpdateBatchKoi(BatchKoi batchKoi)
        {
            return await _batchKoiRepository.UpdateBatchKoiAsync(batchKoi);
        }

        public async Task<bool> DeleteBatchKoi(int id)
        {
            return await _batchKoiRepository.DeleteBatchKoiAsync(id);
        }

        public async Task<BatchKoi> GetBatchKoiById(int id)
        {
            return await _batchKoiRepository.GetBatchKoiByIdAsync(id);
        }
    }

}
