using AutoMapper;
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
    class BatchKoiService : IBatchKoiService
    {
        private readonly IMapper _mapper;
        private readonly IBatchKoiRepository _repository;
        public BatchKoiService(IBatchKoiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BatchKoiDto>> GetAllBatchKoi()
        {
            var allBatch = await _repository.GetAllBatch();
            var allBatchDto = _mapper.Map<IEnumerable<BatchKoiDto>>(allBatch);
            return allBatchDto;
        }

        public async Task<IEnumerable<BatchKoiDto>> GetAllBatchKoiWithCondition(KoiFilterDto koiFilterDto)
        {
            var allBatchKoi = await _repository.GetBatchKoiWithCondition(koiFilterDto.KoiName, koiFilterDto.TypeFish, koiFilterDto.From, koiFilterDto.To, koiFilterDto.SortBy, koiFilterDto.PageNumber, koiFilterDto.PageSize);
            var allBatchKoiDto = _mapper.Map<IEnumerable<BatchKoiDto>>(allBatchKoi);
            return allBatchKoiDto;
        }
    }
}
