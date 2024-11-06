﻿using AutoMapper;
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
        public async Task<IEnumerable<ReviewDtos>> GetReviewByStaff()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<ReviewDtos>();
            }
            var review = await _reviewRepository.GetReviewByStaff();
            var reviewDto = _mapper.Map<IEnumerable<ReviewDtos>>(review);
            return reviewDto;
        }
        public async Task<IEnumerable<OrderDetailDtos>> GetAllOrderDetail()
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if (userId == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var orderDetail = await _reviewRepository.GetAllOrderDetail();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDtos>>(orderDetail);
            if (orderDetailDto == null)
            {
                return Enumerable.Empty<OrderDetailDtos>();
            }
            return orderDetailDto;
        }

        public async Task<bool> AddReview(ReviewDtoComment reviewdto)
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
        public async Task<bool> AddReviewStars(ReviewDtoStar reviewdto)
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
            var result = await _reviewRepository.AddReviewStars(reviews);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AddAllReview(ReviewAllDto reviewdto)
        {
            if (_userContext.GetCurrentUser() == null || _userStore == null)
            {
                throw new ArgumentException("User context or user store is not valid.");
            }
            var userId = _userContext.GetCurrentUser().Id;
            if ((reviewdto.Rating == null || reviewdto.Rating <= 0) && string.IsNullOrWhiteSpace(reviewdto.Comments))
            {
                return false ;
            }
            if (userId == null || (reviewdto.KoiId == null && reviewdto.BatchKoiId == null) || (reviewdto.KoiId.HasValue  && reviewdto.BatchKoiId.HasValue))
            {
                return false;
            }
            var reviews = _mapper.Map<Review>(reviewdto);
            var result = await _reviewRepository.AddAllReview(reviews);
            if (result)
                return true;
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
