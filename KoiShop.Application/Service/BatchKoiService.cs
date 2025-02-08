using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
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
        private readonly IBatchKoiRepository _batchKoiRepository;
        private readonly IFishRepository _koiRepository;

        public BatchKoiService(IBatchKoiRepository batchKoiRepository, IFishRepository koiRepository, IMapper mapper)
        {
            _koiRepository = koiRepository;
            _batchKoiRepository = batchKoiRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BatchKoiDto>> GetAllBatchKoi()
        {
            var allBatch = await _koiRepository.GetFishFormType<BatchKoi>(Variables.STATUS_FISH_ALL);
            var allBatchDto = _mapper.Map<IEnumerable<BatchKoiDto>>(allBatch);
            return allBatchDto;
        }

        public async Task<BatchKoiDto> GetBatchKoi(int id)
        {
            var batchKoi = await _koiRepository.GetFishByIdFromType<BatchKoi>(id,Variables.STATUS_FISH_ONSALE);
            var batchKoidto = _mapper.Map<BatchKoiDto>(batchKoi);
            return batchKoidto;
        }

        public async Task<IEnumerable<BatchKoiDto>> GetAllBatchKoiWithCondition(KoiFilterDto koiFilterDto)
        {
            var allBatchKoi = await _batchKoiRepository.GetBatchKoiWithCondition(koiFilterDto.KoiName, koiFilterDto.TypeFish, koiFilterDto.From, koiFilterDto.To, koiFilterDto.SortBy, koiFilterDto.PageNumber, koiFilterDto.PageSize);
            var allBatchKoiDto = _mapper.Map<IEnumerable<BatchKoiDto>>(allBatchKoi);
            return allBatchKoiDto;
        }

    }
    
}

