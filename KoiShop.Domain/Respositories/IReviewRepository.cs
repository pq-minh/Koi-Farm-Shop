using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReview();
        Task<IEnumerable<Review>> GetReviewByStaff();
        Task<bool> AddReview(Review reviews);
        Task<bool> DeleteReview(int id);
        Task<IEnumerable<T>> GetKoiOrBatch<T>();
        Task<IEnumerable<OrderDetail>> GetAllOrderDetail();
        Task<bool> AddReviewStars(Review reviews);
        Task<bool> AddAllReview(Review reviews);
    }
}
