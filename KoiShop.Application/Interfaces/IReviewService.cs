﻿using KoiShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDtos>> GetReview();
        Task<IEnumerable<ReviewDtos>> GetReviewByStaff();
        Task<bool> AddReview(ReviewDtoComment reviewdto);
        Task<bool> AddReviewStars(ReviewDtoStar reviewdto);
        Task<bool> AddAllReview(ReviewAllDto reviewdto);
        Task<bool> DeleteReview(int? id);
        Task<IEnumerable<OrderDetailDtos>> GetAllOrderDetail();
        Task<IEnumerable<KoiDto>> GetKoiFromOrderDetail();
        Task<IEnumerable<BatchKoiDto>> GetBatchFromOrderDetail();
    }
}
