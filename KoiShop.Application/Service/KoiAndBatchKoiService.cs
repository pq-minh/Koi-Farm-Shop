using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    public class KoiAndBatchKoiService : IKoiAndBatchKoiService
    {
        private readonly IMapper _mapper;
        private readonly IKoiService _koiService;
        private readonly IBatchKoiService _batchKoiService;
        public KoiAndBatchKoiService(IMapper mapper, IKoiService koiService, IBatchKoiService batchKoiService)
        {
            _mapper = mapper;
            _koiService = koiService;
            _batchKoiService = batchKoiService;
        }
        public async Task<KoiAndBatchKoiDto> GetAllKoiAndBatch(KoiFilterDto koiFilterDto)
        {
            var allKoiDto = await _koiService.GetAllKoiWithCondition(koiFilterDto) ?? Enumerable.Empty<KoiDto>();

            var allBatchKoiDto = await _batchKoiService.GetAllBatchKoiWithCondition(koiFilterDto) ?? Enumerable.Empty<BatchKoiDto>();

            var result = new KoiAndBatchKoiDto
            {
                Koi = allKoiDto,
                BatchKoi = allBatchKoiDto
            };

            return result;
        }
    }
}
