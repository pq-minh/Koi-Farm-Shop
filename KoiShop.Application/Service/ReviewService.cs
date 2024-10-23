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
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IUserContext userContext, IUserStore<User> userStore)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _userContext = userContext;
            _userStore = userStore;
        }
        public async Task<IEnumerable<ReviewDtos>> GetReview()
        {
            var review = await _reviewRepository.GetReview();
            var reviewDto = _mapper.Map<IEnumerable<ReviewDtos>>(review);
            return reviewDto;
        }

        public async Task<bool> AddReview (ReviewDtos reviewdto)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || reviewdto == null)
            {
                return false;
            }
            var reviews = _mapper.Map<Review>(reviewdto);
            var result = await _reviewRepository.AddReview(reviews);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteReview(int? id)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null || id == null)
            {
                return false;
            }
            var result = await _reviewRepository.DeleteReview((int)id);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<KoiDto>> GetKoiFromOrderDetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<KoiDto>();
            }
            var koi = await _reviewRepository.GetKoiOrBatch<Koi>();
            var koiDto = _mapper.Map<IEnumerable<KoiDto>>(koi);
            return koiDto;
        }
        public async Task<IEnumerable<BatchKoiDto>> GetBatchFromOrderDetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<BatchKoiDto>();
            }
            var batch = await _reviewRepository.GetKoiOrBatch<BatchKoi>();
            var batchDto = _mapper.Map<IEnumerable<BatchKoiDto>>(batch);
            return batchDto;
        }
    }
}
