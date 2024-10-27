using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    public class KoiService : IKoiService 
    {
        private readonly IMapper _mapper;
        private readonly IKoiRepository _koiRepository;
        private readonly FirebaseService _firebaseService;

        public KoiService(IKoiRepository koiRepository, IMapper mapper)
        {
            _koiRepository = koiRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KoiDto>> GetAllKoi()
        {
            var allkoi = await _koiRepository.GetAllKois();
            var allKoiDto = _mapper.Map<IEnumerable<KoiDto>>(allkoi);
            return allKoiDto;
        }
        public async Task<KoiDto> GetKoi(int id)
        {
            var koi = await _koiRepository.GetKoi(id);
            var koidto = _mapper.Map<KoiDto>(koi);
            return koidto;
        }
        public async Task<IEnumerable<KoiDto>> GetAllKoiWithCondition(KoiFilterDto koiFilterDto)
        {
            var allKoi = await _koiRepository.GetKoiWithCondition(koiFilterDto.KoiName, koiFilterDto.TypeFish, koiFilterDto.From, koiFilterDto.To, koiFilterDto.SortBy, koiFilterDto.PageNumber, koiFilterDto.PageSize);
            var allKoiDto = _mapper.Map<IEnumerable<KoiDto>>(allKoi);

            return allKoiDto;
        }
    }
}
