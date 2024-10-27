using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Migrations;
using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Infrastructure.Respositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly KoiShopV1DbContext _koiShopV1DbContext;
        private readonly IUserStore<User> _userStore;
        private readonly IUserContext _userContext;
        public ReviewRepository(KoiShopV1DbContext koiShopV1DbContext, IUserStore<User> userStore, IUserContext userContext)
        {
            _koiShopV1DbContext = koiShopV1DbContext;
            _userStore = userStore;
            _userContext = userContext;
        }
        public async Task<IEnumerable<Review>> GetReview()
        {
            var post = await _koiShopV1DbContext.Reviews.ToListAsync();
            if (post == null)
            {
                return Enumerable.Empty<Review>();
            }
            return post;
        }
        public async Task<bool> AddReview(Review reviews)
        {
            var user = _userContext.GetCurrentUser();
            if (reviews == null || user.Id == null)
            {
                return false;
            }
            int? batchKoiId = null;
            int? koiId = null;
            if (reviews.KoiId == null)
            {
                batchKoiId = reviews.BatchKoiId;
            }
            else if (reviews.BatchKoiId == null || reviews.BatchKoiId == 0)
            {
                koiId = reviews.KoiId;
            }
            else if (batchKoiId == null && koiId == null)
            {
                return false;
            }
            var review = new Review
            {
                BatchKoiId = batchKoiId,
                KoiId = koiId,
                Rating = reviews.Rating,
                Comments = reviews.Comments,
                UserId = user.Id,
                CreateDate = DateTime.Now,
            };
            _koiShopV1DbContext.Reviews.Add(review);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddReviewStars(Review reviews)
        {
            var review = await _koiShopV1DbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == reviews.ReviewId);
            if (review == null)
            {
                return false;
            }
            if (reviews.Rating < 1 || reviews.Rating > 5)
            {
                return false;
            }
            review.Rating = reviews.Rating;
            _koiShopV1DbContext.Reviews.Update(review);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReview(int id)
        {
            var user = _userContext.GetCurrentUser();
            if (id == null || user.Id == null)
            {
                return false;
            }
            var review = await _koiShopV1DbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
            if (review == null)
            {
                return false;
            }
            _koiShopV1DbContext.Reviews.Remove(review);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail()
        {
            var userId = _userContext.GetCurrentUser().Id;
            var order = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).Select(o => o.OrderId).ToListAsync();
            if (order == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => order.Contains((int)od.OrderId)).ToListAsync();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            return orderDetail;
        }
        public async Task<IEnumerable<T>> GetKoiOrBatch<T>()
        {
            var userId = _userContext.GetCurrentUser().Id;
            var orders = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
            if (orders == null)
            {
                return Enumerable.Empty<T>();
            }

            var orderIds = orders.Select(o => o.OrderId).ToList();
            var fish = await _koiShopV1DbContext.OrderDetails
                .Where(od => orderIds.Contains((int)od.OrderId))
                .ToListAsync();
            if (fish == null)
            {
                return Enumerable.Empty<T>();
            }
            var batch = await _koiShopV1DbContext.BatchKois.Where(b => fish.Any(f => b.BatchKoiId == f.BatchKoiId)).ToListAsync();
            var koi = await _koiShopV1DbContext.Kois.Where(k => fish.Any(f => k.KoiId == f.KoiId)).ToListAsync();
            if (typeof(T) == typeof(BatchKoi))
            {
                return batch as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(Koi))
            {
                return koi as IEnumerable<T>;
            }
            return Enumerable.Empty<T>();
        }
    }
}
