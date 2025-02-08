using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Interfaces;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;


namespace KoiShop.Application.Service
{
    public class KoiService : IKoiService 
    {
        List<string> koiStatus = new(){ "OnSale", "Sold", "Pending", "Cancel" };
        private readonly IMapper _mapper;
        private readonly IFishRepository _fishRepository;

        public KoiService(IFishRepository fishRepository, IMapper mapper)
        {
            _fishRepository = fishRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KoiDto>> GetAllKoi()
        {
            var allkoi = await _fishRepository.GetFishFormType<Koi>(Variables.STATUS_FISH_ONSALE);
            var allKoiDto = _mapper.Map<IEnumerable<KoiDto>>(allkoi);
            return allKoiDto;
        }
        public async Task<KoiDto> GetKoi(int id)
        {
            var koi = await _fishRepository.GetFishByIdFromType<Koi>(id, Variables.STATUS_FISH_ONSALE);
            var koidto = _mapper.Map<KoiDto>(koi);
            return koidto;
        }
        public async Task<IEnumerable<KoiDto>> GetAllKoiWithCondition(KoiFilterDto koiFilterDto)
        {
            var allKoi = await _fishRepository.GetKoiWithCondition(koiFilterDto.KoiName, koiFilterDto.TypeFish, koiFilterDto.From, koiFilterDto.To, koiFilterDto.SortBy, koiFilterDto.PageNumber, koiFilterDto.PageSize);
            var allKoiDto = _mapper.Map<IEnumerable<KoiDto>>(allKoi);

            return allKoiDto;
        }

        public async Task<bool> UpdateFishStatus(int koiId, string status)
        {
            if (!koiStatus.Contains(status)) return false;

            var currentKoi = await _fishRepository.GetFishByIdFromType<Koi>(koiId, Variables.STATUS_FISH_ALL);
            if (currentKoi == null) return false;

            currentKoi.Status = status;
            var result = await _fishRepository.UpdateFish(currentKoi);

            return result;
        }

    }

}