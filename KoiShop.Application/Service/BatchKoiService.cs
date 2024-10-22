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
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Service
{
    class BatchKoiService : IBatchKoiService
    {
        private readonly IMapper _mapper;
        private readonly IBatchKoiRepository _batchKoiRepository;
        private readonly FirebaseService _firebaseService;
        public BatchKoiService(IBatchKoiRepository batchKoiRepository, FirebaseService firebaseService, IMapper mapper)
        {
            _batchKoiRepository = batchKoiRepository;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BatchKoiDto>> GetAllBatchKoi()
        {
            var allBatch = await _batchKoiRepository.GetAllBatch();
            var allBatchDto = _mapper.Map<IEnumerable<BatchKoiDto>>(allBatch);
            return allBatchDto;
        }

        public async Task<BatchKoiDto> GetBatchKoi(int id)
        {
            var koi = await _batchKoiRepository.GetBatchKoi(id);
            var koidto = _mapper.Map<BatchKoiDto>(koi);
            return koidto;
        }

        public async Task<IEnumerable<BatchKoiDto>> GetAllBatchKoiWithCondition(KoiFilterDto koiFilterDto)
        {
            var allBatchKoi = await _batchKoiRepository.GetBatchKoiWithCondition(koiFilterDto.KoiName, koiFilterDto.TypeFish, koiFilterDto.From, koiFilterDto.To, koiFilterDto.SortBy, koiFilterDto.PageNumber, koiFilterDto.PageSize);
            var allBatchKoiDto = _mapper.Map<IEnumerable<BatchKoiDto>>(allBatchKoi);
            return allBatchKoiDto;
        }


        // Staff =====================

        // BatchKoi Methods ===========================================================================================
        public async Task<IEnumerable<BatchKoi>> GetAllBatchKoiStaff()
        {
            return await _batchKoiRepository.GetAllBatchKoiAsync();
        }

        public async Task<bool> AddBatchKoi(AddBatchKoiDto batchKoiDto)
        {
            var batchKoi = _mapper.Map<BatchKoi>(batchKoiDto);

            var koiImageUrl = await _firebaseService.UploadFileToFirebaseStorage(batchKoiDto.KoiImage, "KoiFishImage");
            var cerImageUrl = await _firebaseService.UploadFileToFirebaseStorage(batchKoiDto.Certificate, "KoiFishCertificate");

            batchKoi.Image = koiImageUrl;
            batchKoi.Certificate = cerImageUrl;
            return await _batchKoiRepository.AddBatchKoiAsync(batchKoi);
        }

        public async Task<bool> UpdateBatchKoi(BatchKoi batchKoi)
        {
            return await _batchKoiRepository.UpdateBatchKoiAsync(batchKoi);
    }
        public async Task<BatchKoi> GetBatchKoiById(int id)
        {
            return await _batchKoiRepository.GetBatchKoiByIdAsync(id);
        }

        public async Task<bool> ValidateBatchTypeIdInBatchKoi(int batchTypeId)
        {
            if (batchTypeId < 1) return false;
            // ktra xem có tồn tại BtachTypeId trong BatchKoi
            var list = await GetAllBatchKoiCategory();
            bool contain = false;
            foreach (var item in list)
            {
                if (batchTypeId == item.BatchTypeId)
                {
                    contain = true;
                }
            }
            if (!contain) return false;

            return true;
        }

        public async Task<BatchKoi> ValidateUpdateBatchKoiInfo(int batchKoiId, UpdateBatchKoiDto batchKoiDto)
        {
            // lấy cá koi cần update từ database 
            var currentBatchKoi = await GetBatchKoiById(batchKoiId);
            if (currentBatchKoi == null)
                return null;

            // so sánh koi từ db vs koi gửi từ frontend
            // nếu property nào có tt hợp lệ gửi từ Form thì update, ko thì thôi
            if (batchKoiDto.BatchTypeId.HasValue)
            {
                if (await ValidateBatchTypeIdInBatchKoi((int)batchKoiDto.BatchTypeId))
                    currentBatchKoi.BatchTypeId = batchKoiDto.BatchTypeId;
                else
                    return null;
            }

            if (!string.IsNullOrEmpty(batchKoiDto.BatchKoiName)) currentBatchKoi.Name = batchKoiDto.BatchKoiName;
            if (!string.IsNullOrEmpty(batchKoiDto.Origin)) currentBatchKoi.Origin = batchKoiDto.Origin;
            if (!string.IsNullOrEmpty(batchKoiDto.Description)) currentBatchKoi.Description = batchKoiDto.Description;
            if (!string.IsNullOrEmpty(batchKoiDto.Age)) currentBatchKoi.Age = batchKoiDto.Age;
            if (!string.IsNullOrEmpty(batchKoiDto.Gender)) currentBatchKoi.Gender = batchKoiDto.Gender;
            if (!string.IsNullOrEmpty(batchKoiDto.Quantity)) currentBatchKoi.Quantity = batchKoiDto.Quantity;
            if (!string.IsNullOrEmpty(batchKoiDto.Weight)) currentBatchKoi.Weight = batchKoiDto.Weight;
            if (!string.IsNullOrEmpty(batchKoiDto.Size)) currentBatchKoi.Size = batchKoiDto.Size;
            if (!string.IsNullOrEmpty(batchKoiDto.Status)) currentBatchKoi.Status = batchKoiDto.Status;
            if (batchKoiDto.Price.HasValue && batchKoiDto.Price > 0) currentBatchKoi.Price = batchKoiDto.Price;

            string batchKoiImage = await ValidateImage(batchKoiDto.KoiImage, currentBatchKoi.Image, "KoiFishImage");
            string batchKoiCertificate = await ValidateImage(batchKoiDto.Certificate, currentBatchKoi.Certificate, "KoiFishCertificate");

            if(!string.IsNullOrEmpty(batchKoiImage))
                currentBatchKoi.Image = batchKoiImage;

            if (!string.IsNullOrEmpty(batchKoiCertificate))
                currentBatchKoi.Certificate = batchKoiCertificate;
            
            return currentBatchKoi;
        }

        public async Task<string> ValidateImage(IFormFile image, string oldImagePath, string path)
        {
            string currentImagePath = null;
            if (image != null)
            {
                string imagePath = _firebaseService.GetRelativeFilePath(oldImagePath);
                if (imagePath != null)       // xóa ảnh cũ trong firebase
                    await _firebaseService.DeleteFileInFirebaseStorage(imagePath);

                // upload ảnh mới lên firebase
                currentImagePath = await _firebaseService.UploadFileToFirebaseStorage(image, path);
                if (currentImagePath == null)
                    return null;
            }
            return currentImagePath;
        }

        public async Task<bool> UpdateBatchKoiStatus(int batchkoiId, string status)
        {
            var batchKoi = await _batchKoiRepository.GetBatchKoiByIdAsync(batchkoiId);
            if (batchKoi == null) return false;

            batchKoi.Status = status;

            return await _batchKoiRepository.UpdateBatchKoiAsync(batchKoi); ;
        }

        // BatchKoiCategory Methods ====================================================================================

        public async Task<IEnumerable<BatchKoiCategory>> GetAllBatchKoiCategory()
        {
            return await _batchKoiRepository.GetAllBatchKoiCategoryAsync();
        }

        public async Task<List<BatchKoi>> GetBatchKoiInBatchKoiCategory(int batchTypeId)
        {
            return await _batchKoiRepository.GetBatchKoiInBatchKoiCategoryAsync(batchTypeId);
        }
    }
    
}

