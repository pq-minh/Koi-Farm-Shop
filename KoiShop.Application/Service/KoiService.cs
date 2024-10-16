using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
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
        private readonly FirebaseService _firebaseService;
        private readonly IMapper _mapper;

        public KoiService(IKoiRepository koiRepository, FirebaseService firebaseService, IMapper mapper)
        {
            _koiRepository = koiRepository;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KoiDto>> GetAllKoi()
        {
            var allkoi = await _koiRepository.GetAllKois();
            var allKoiDto = _mapper.Map<IEnumerable<KoiDto>>(allkoi);
            return allKoiDto;
        }
        public async Task<KoiDto> GetKoi (int id)
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

        // Staff =====================
        // Koi Methods =============================================================================================
        public async Task<IEnumerable<Koi>> GetAllKoiStaff()
        {
            return await _koiRepository.GetAllKoiAsync();
        }

        public async Task<Koi> GetKoiById(int koiId)
        {
            return await _koiRepository.GetKoiByIdAsync(koiId);
        }

        public async Task<bool> AddKoi(AddKoiDto koiDto, string imageUrl)
        {
            var koi = _mapper.Map<Koi>(koiDto);
            koi.Image = imageUrl;
            return await _koiRepository.AddKoiAsync(koi);
        }

        public async Task<bool> UpdateKoi(Koi koi)
        {
            return await _koiRepository.UpdateKoiAsync(koi);
        }

        public async Task<bool> ValidateAddKoiDtoInfo(AddKoiDto koiDto)
        {
            if (koiDto == null) return false;

            if (!await ValidateFishTypeIdInKoi(koiDto.FishTypeId)) return false;

            if (string.IsNullOrEmpty(koiDto.Name)) return false;
            if (string.IsNullOrEmpty(koiDto.Origin)) return false;
            if (string.IsNullOrEmpty(koiDto.Description)) return false;
            if (string.IsNullOrEmpty(koiDto.Gender)) return false;
            if (koiDto.ImageFile == null) return false;
            if (koiDto.Age <= 0) return false;
            if (koiDto.Weight <= 0) return false;
            if (koiDto.Size <= 0) return false;
            if (string.IsNullOrEmpty(koiDto.Personality)) return false;
            if (string.IsNullOrEmpty(koiDto.Status)) return false;
            if (koiDto.Price <= 0) return false;
            if (string.IsNullOrEmpty(koiDto.Certificate)) return false;

            return true;
        }
        public async Task<bool> ValidateFishTypeIdInKoi(int FishTypeId)
        {
            if (FishTypeId < 1) return false;
            // ktra xem có tồn tại FishTypeId trong Koi
            var KgList = await GetAllKoiCategory();
            bool contain = false;
            foreach (var item in KgList)
            {
                if (FishTypeId == item.FishTypeId)
                {
                    contain = true;
                }
            }
            if (!contain) return false;

            return true;
        }

        public async Task<Koi> ValidateUpdateKoiDto(int koiId, UpdateKoiDto koiDto)
        {
            // lấy cá koi cần update từ database 
            var currentKoi = await GetKoiById(koiId);
            if (currentKoi == null)
                return null;

            // so sánh koi từ db vs koi gửi từ frontend
            // nếu property nào có tt hợp lệ gửi từ Form thì update, ko thì thôi
            if (koiDto.FishTypeId.HasValue)
            {
                if (await ValidateFishTypeIdInKoi((int)koiDto.FishTypeId))
                    currentKoi.FishTypeId = koiDto.FishTypeId;
                else
                    return null;
            }

            if (!string.IsNullOrEmpty(koiDto.KoiName)) currentKoi.Name = koiDto.KoiName;
            if (!string.IsNullOrEmpty(koiDto.Origin)) currentKoi.Origin = koiDto.Origin;
            if (!string.IsNullOrEmpty(koiDto.Description)) currentKoi.Description = koiDto.Description;
            if (!string.IsNullOrEmpty(koiDto.Gender)) currentKoi.Gender = koiDto.Gender;
            if (koiDto.Age.HasValue && koiDto.Age > 0) currentKoi.Age = koiDto.Age;
            if (koiDto.Weight.HasValue && koiDto.Weight > 0) currentKoi.Weight = koiDto.Weight;
            if (koiDto.Size.HasValue && koiDto.Size > 0) currentKoi.Size = koiDto.Size;
            if (!string.IsNullOrEmpty(koiDto.Personality)) currentKoi.Personality = koiDto.Personality;
            if (!string.IsNullOrEmpty(koiDto.Status)) currentKoi.Status = koiDto.Status;
            if (koiDto.Price.HasValue && koiDto.Price > 0) currentKoi.Price = koiDto.Price;
            if (!string.IsNullOrEmpty(koiDto.Certificate)) currentKoi.Certificate = koiDto.Certificate;

            // update image
            if (koiDto.ImageFile != null)
            {
                string oldImagePath = currentKoi.Image;

                // tách đg dẫn tuyệt đối thành đg dẫn tg đối  vì Firebase chỉ nhận vào đg dẫn tg đối
                var startIndex = oldImagePath.IndexOf("/o/") + 3; // 3 là độ dài của chuỗi "/o/"
                var endIndex = oldImagePath.IndexOf("?");
                var filePath = Uri.UnescapeDataString(oldImagePath.Substring(startIndex, endIndex - startIndex));

                // xóa ảnh cũ trong firebase
                var result = await _firebaseService.DeleteFileInFirebaseStorageAsync(filePath);
                if (!result)
                    return null;

                // tải ảnh mới lên firebase
                currentKoi.Image = await _firebaseService.UploadFileToFirebaseStorageAsync(koiDto.ImageFile, "KoiFishImage");

                if (currentKoi.Image == null)
                    return null;
            }

            return currentKoi;
        }

        // KoiCategory Methods ======================================================================================
        public async Task<IEnumerable<KoiCategory>> GetAllKoiCategory()
        {
            return await _koiRepository.GetAllKoiCategoryAsync();
        }

        public async Task<List<Koi>> GetKoiInKoiCategory(int fishTypeId)
        {
            return await _koiRepository.GetKoiInKoiCategoryAsync(fishTypeId);
        }

    }
}
