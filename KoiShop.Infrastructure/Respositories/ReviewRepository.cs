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
            var post = await _koiShopV1DbContext.Reviews.Where(r => r.Status == "Posted").Include(r => r.Koi).Include(r => r.BatchKoi).Include(r => r.User).ToListAsync();
            if (post == null)
            {
                return Enumerable.Empty<Review>();
            }
            return post;
        }
        public async Task<IEnumerable<Review>> GetReviewByStaff()
        {
            var post = await _koiShopV1DbContext.Reviews.Include(r => r.Koi).Include(r => r.BatchKoi).Include(r => r.User).ToListAsync();
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
            var review = await _koiShopV1DbContext.Reviews.Where(r => (r.KoiId == reviews.KoiId && r.BatchKoiId == null)
            || (r.BatchKoiId == reviews.BatchKoiId && r.KoiId == null) && r.UserId == user.Id)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                var reviewnew = new Review
                {
                    BatchKoiId = reviews.BatchKoiId,
                    KoiId = reviews.KoiId,
                    Comments = reviews.Comments,
                    UserId = user.Id,
                    CreateDate = DateTime.Now,
                    Status = "Posted"
                };
                _koiShopV1DbContext.Reviews.Add(reviewnew);
            }
            else
            {
                review.Comments = reviews.Comments ?? review.Comments;
                review.CreateDate = DateTime.Now;
                _koiShopV1DbContext.Reviews.Update(review);
            }
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddReviewStars(Review reviews)
        {
            var user = _userContext.GetCurrentUser();
            if (reviews == null || user == null)
            {
                return false;
            }
            var review = await _koiShopV1DbContext.Reviews.Where(r => (r.KoiId == reviews.KoiId && r.BatchKoiId == null)
            || (r.BatchKoiId == reviews.BatchKoiId && r.KoiId == null) && r.UserId == user.Id)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                var reviewnew = new Review
                {
                    KoiId = reviews.KoiId,
                    BatchKoiId = reviews.BatchKoiId,
                    UserId = user.Id,
                    CreateDate = DateTime.Now
                };
                _koiShopV1DbContext.Reviews.Add(reviewnew);
                await _koiShopV1DbContext.SaveChangesAsync();
            }

            if (reviews.Rating < 1 || reviews.Rating > 5)
            {
                return false;
            }
            review.Rating = reviews.Rating;
            review.CreateDate = DateTime.Now;
            _koiShopV1DbContext.Reviews.Update(review);
            await _koiShopV1DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAllReview(Review reviews)
        {
            var user = _userContext.GetCurrentUser();
            if (reviews == null || user.Id == null)
            {
                return false;
            }

            var review = await _koiShopV1DbContext.Reviews.Where(r => (r.KoiId == reviews.KoiId && r.BatchKoiId == null)
            || (r.BatchKoiId == reviews.BatchKoiId && r.KoiId == null) && r.UserId == user.Id)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                var reviewnew = new Review
                {
                    BatchKoiId = reviews.BatchKoiId,
                    KoiId = reviews.KoiId,
                    Comments = reviews.Comments,
                    UserId = user.Id,
                    CreateDate = DateTime.Now,
                    Rating = reviews.Rating,
                    Status = "Posted"
                };
                _koiShopV1DbContext.Reviews.Add(reviewnew);
            }
            else
            {
                review.Comments = reviews.Comments ?? review.Comments;
                review.CreateDate = DateTime.Now;
                int? rating = review.Rating;
                if (reviews.Rating != null && reviews.Rating >= 0 && reviews.Rating <= 5 && reviews.Rating != review.Rating)
                    rating = reviews.Rating;
                review.Rating = rating;
                _koiShopV1DbContext.Reviews.Update(review);
            }
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
            review.Status = "Removed";
            _koiShopV1DbContext.Reviews.Update(review);
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
            var orderDetail = await _koiShopV1DbContext.OrderDetails.Where(od => order.Contains((int)od.OrderId)).Include(od => od.Koi).Include(od => od.BatchKoi).ToListAsync();
            if (orderDetail == null)
            {
                return Enumerable.Empty<OrderDetail>();
            }
            return orderDetail;
        }
        public async Task<IEnumerable<T>> GetKoiOrBatch<T>()
        {
            var userId = _userContext.GetCurrentUser().Id;
            var orderIds = await _koiShopV1DbContext.Orders.Where(o => o.UserId == userId).Select(o => o.OrderId).ToListAsync();
            if (orderIds == null)
            {
                return Enumerable.Empty<T>();
            }
            var fish = await _koiShopV1DbContext.OrderDetails.Where(od => orderIds.Contains((int)od.OrderId)).ToListAsync();

            if (fish == null || !fish.Any())
            {
                return Enumerable.Empty<T>();
            }
            var batchKoiIds = fish.Where(f => f.BatchKoiId.HasValue).Select(f => f.BatchKoiId.Value).ToList();
            var koiIds = fish.Where(f => f.KoiId.HasValue).Select(f => f.KoiId.Value).ToList();

            List<BatchKoi> batch = null;
            List<Koi> koi = null;

            if (typeof(T) == typeof(BatchKoi))
            {
                batch = await _koiShopV1DbContext.BatchKois
                    .Where(b => batchKoiIds.Contains(b.BatchKoiId))
                    .ToListAsync();
                return batch as IEnumerable<T>;
            }
            else if (typeof(T) == typeof(Koi))
            {
                koi = await _koiShopV1DbContext.Kois
                    .Where(k => koiIds.Contains(k.KoiId))
                    .ToListAsync();
                return koi as IEnumerable<T>;
            }

            return Enumerable.Empty<T>();
        }
    }
}
