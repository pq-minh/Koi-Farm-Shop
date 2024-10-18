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

        public async Task<bool> AddKoi(AddKoiDto koiDto, string koiImageUrl, string cerImageUrl)
        {
            var koi = _mapper.Map<Koi>(koiDto);
            koi.Image = koiImageUrl;
            koi.Certificate = cerImageUrl;
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

            string koiImage = await ValidateKoiImage(koiDto.ImageFile, currentKoi.Image, "KoiFishImage");
            string koiCertificate = await ValidateKoiImage(koiDto.ImageFile, currentKoi.Certificate, "KoiFishCertificate");

            if(koiImage == null || koiCertificate == null)
                return null;

            currentKoi.Image = koiImage;
            currentKoi.Certificate = koiCertificate;    

            return currentKoi;
        }


        public async Task<string> ValidateKoiImage(IFormFile image, string oldImagePath, string path)
        {
            string currentImagePath = null;
            if (image != null)
            {
                string filePath = await _firebaseService.GetRelativeFilePath(oldImagePath);
                if (filePath != null)       // xóa ảnh cũ trong firebase      
                    await _firebaseService.DeleteFileInFirebaseStorageAsync(filePath);

                // upload ảnh mới lên firebase
                currentImagePath = await _firebaseService.UploadFileToFirebaseStorageAsync(image, path);
                if (currentImagePath == null)
                    return null;
            }
            return currentImagePath;
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
