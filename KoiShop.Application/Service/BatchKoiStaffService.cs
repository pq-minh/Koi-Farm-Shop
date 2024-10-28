using AutoMapper;
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
    public class BatchKoiStaffService : IBatchKoiStaffService
    {
        private readonly IMapper _mapper;
        private readonly IBatchKoiRepository _batchKoiRepository;
        private readonly FirebaseService _firebaseService;
        public BatchKoiStaffService(IBatchKoiRepository batchKoiRepository , FirebaseService firebaseService, IMapper mapper)
        {
            _batchKoiRepository = batchKoiRepository;
            _firebaseService = firebaseService;
            _mapper = mapper;
        }

        // BatchKoi Methods ===========================================================================================
        public async Task<IEnumerable<BatchKoi>> GetAllBatchKoiStaff()
        {
            return await _batchKoiRepository.GetBatchKois();
        }

        public async Task<BatchKoi> GetBatchKoiById(int batchKoiId)
        {
            return await _batchKoiRepository.GetBatchKoiById(batchKoiId);
        }

        public async Task<bool> AddBatchKoi(AddBatchKoiDto batchKoiDto)
        {
            if (batchKoiDto == null) return false;

            var allCates = await _batchKoiRepository.GetBatchKoiCategories();
            bool exist = allCates.Any(cate => cate.BatchTypeId == batchKoiDto.BatchTypeId);

            if (!exist)
                return false;

            var koiImageUrl = await _firebaseService.UploadFileToFirebaseStorage(batchKoiDto.KoiImage, "KoiFishImage");
            var cerImageUrl = await _firebaseService.UploadFileToFirebaseStorage(batchKoiDto.Certificate, "KoiFishCertificate");

            var batchKoi = new BatchKoi
            {
                BatchTypeId = batchKoiDto.BatchTypeId,
                Name = batchKoiDto.Name,
                Description = batchKoiDto.Description,
                Quantity = batchKoiDto.Quantity,
                Weight = batchKoiDto.Weight,
                Size = batchKoiDto.Size,
                Origin = batchKoiDto.Origin,
                Gender = batchKoiDto.Gender,
                Age = batchKoiDto.Age,
                Certificate = cerImageUrl,
                Image = koiImageUrl,
                Price = batchKoiDto.Price,
                Status = batchKoiDto.Status
            };

            return await _batchKoiRepository.AddBatchKoi(batchKoi);
        }

        public async Task<bool> UpdateBatchKoi(UpdateBatchKoiDto batchKoiDto)
        {
            if (batchKoiDto == null) return false;

            var currentKoi = await _batchKoiRepository.GetBatchKoiById(batchKoiDto.BatchKoiId);
            if (currentKoi == null) return false;

            if (batchKoiDto.BatchTypeId.HasValue)
            {
                var allCates = await _batchKoiRepository.GetBatchKoiCategories();
                bool exist = allCates.Any(cate => cate.BatchTypeId == batchKoiDto.BatchTypeId.Value);

                if (exist)
                    currentKoi.BatchTypeId = batchKoiDto.BatchTypeId.Value;
                else
                    return false;
            }

            if (!string.IsNullOrEmpty(batchKoiDto.Name)) currentKoi.Name = batchKoiDto.Name;
            if (!string.IsNullOrEmpty(batchKoiDto.Description)) currentKoi.Description = batchKoiDto.Description;
            if (!string.IsNullOrEmpty(batchKoiDto.Quantity)) currentKoi.Quantity = batchKoiDto.Quantity;
            if (!string.IsNullOrEmpty(batchKoiDto.Weight)) currentKoi.Weight = batchKoiDto.Weight;
            if (!string.IsNullOrEmpty(batchKoiDto.Size)) currentKoi.Size = batchKoiDto.Size;
            if (!string.IsNullOrEmpty(batchKoiDto.Origin)) currentKoi.Origin = batchKoiDto.Origin;
            if (!string.IsNullOrEmpty(batchKoiDto.Gender)) currentKoi.Gender = batchKoiDto.Gender;
            if (!string.IsNullOrEmpty(batchKoiDto.Age)) currentKoi.Age = batchKoiDto.Age;
            if (batchKoiDto.Price.HasValue) currentKoi.Price = batchKoiDto.Price.Value;
            if (!string.IsNullOrEmpty(batchKoiDto.Status)) currentKoi.Status = batchKoiDto.Status;

            if (batchKoiDto.KoiImage != null)
            {
                string koiImage = await UpdateImage(batchKoiDto.KoiImage, currentKoi.Image, "KoiFishImage");
                if (!string.IsNullOrEmpty(koiImage))
                    currentKoi.Image = koiImage;
            }

            if (batchKoiDto.Certificate != null)
            {
                string koiCer = await UpdateImage(batchKoiDto.Certificate, currentKoi.Certificate, "KoiFishCertificate");
                if (!string.IsNullOrEmpty(koiCer))
                    currentKoi.Certificate = koiCer;
            }

            return await _batchKoiRepository.UpdateBatchKoi(currentKoi);
        }


        public async Task<string> UpdateImage(IFormFile imageFile, string oldImagePath, string directory)
        {
            if (imageFile == null)
            {
                return null;
            }

            // sóa ảnh cũ 
            string imagePath = _firebaseService.GetRelativeFilePath(oldImagePath);
            if (!string.IsNullOrEmpty(imagePath))
            {
                await _firebaseService.DeleteFileInFirebaseStorage(imagePath);
            }

            // up ảnh mới
            string newImageUrl = await _firebaseService.UploadFileToFirebaseStorage(imageFile, directory);
            if (string.IsNullOrEmpty(newImageUrl))
            {
                return null;
            }

            return newImageUrl;
        }


        public async Task<bool> UpdateBatchKoiStatus(int batchKoiId, string status)
        {
            var batchKoi = await _batchKoiRepository.GetBatchKoiById(batchKoiId);
            if (batchKoi == null) return false;

            batchKoi.Status = status;

            return await _batchKoiRepository.UpdateBatchKoi(batchKoi);
        }

        // KoiCategory Methods ======================================================================================
        public async Task<IEnumerable<BatchKoiCategory>> GetAllBatchKoiCategory()
        {
            return await _batchKoiRepository.GetBatchKoiCategories();
        }

        public async Task<List<BatchKoi>> GetBatchKoiInBatchKoiCategory(int batchTypeId)
        {
            return await _batchKoiRepository.GetBatchKoiInBatchKoiCategory(batchTypeId);
        }
    }
}
