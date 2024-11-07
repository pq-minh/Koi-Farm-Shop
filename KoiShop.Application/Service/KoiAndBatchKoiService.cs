using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Service
{
    public class KoiAndBatchKoiService : IKoiAndBatchKoiService
    {
        private readonly IMapper _mapper;
        private readonly IKoiService _koiService;
        private readonly IBatchKoiService _batchKoiService;
        private readonly IKoiRepository _koiRepository;
        private readonly IBatchKoiRepository _batchKoiRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        public KoiAndBatchKoiService(IMapper mapper, IKoiService koiService, IBatchKoiService batchKoiService, IKoiRepository koiRepository, IBatchKoiRepository batchKoiRepository, IUserContext userContext, IUserStore<User> userStore)
        {
            _mapper = mapper;
            _koiService = koiService;
            _batchKoiService = batchKoiService;
            _koiRepository = koiRepository;
            _batchKoiRepository = batchKoiRepository;
            _userContext = userContext;
            _userStore = userStore;
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
        public async Task<KoiAndBatchKoiIdDto> GetKoiOrBatchSold(int? koiId, int? batchKoiId)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || (koiId == null && batchKoiId == null))
            {
                return null;
            }
            if (koiId.HasValue && batchKoiId.HasValue)
            {
                return null;
            }
            var koi = koiId.HasValue? await _koiRepository.GetKoiSold(koiId.Value): null;

            var batchKoi = batchKoiId.HasValue? await _batchKoiRepository.GetBatchKoiSold(batchKoiId.Value): null;

            var koiDto = koi != null ? _mapper.Map<KoiDto>(koi) : null;
            var batchKoiDto = batchKoi != null ? _mapper.Map<BatchKoiDto>(batchKoi) : null;
            var result = new KoiAndBatchKoiIdDto
            {
                Koi = koiDto,
                BatchKoi = batchKoiDto
            };
            return result;
        }
    }
}
