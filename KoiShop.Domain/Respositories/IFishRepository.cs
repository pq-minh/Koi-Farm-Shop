using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Respositories
{
    //Sửa lại thành 1 interface gộp lại với Ibatchkoirepo với Ikoirepo tuân thủ desgin pattern
   public interface IFishRepository
    {
        Task<IEnumerable<T>> GetAllFish<T>() where T : class;
        Task<IEnumerable<T>> GetFishFormType<T>(string type) where T : class;
        Task<T?> GetFishByIdFromType<T>(int id, string type) where T : class;
        Task<IEnumerable<Koi>> GetKoiWithCondition(string koiName, string typeFish, double? from, double? to, string sortBy, int pageNumber, int pageSize);

        // Koi Methods ============================================================================================

        Task<bool> AddFish<T>(T fish) where T : class;
        Task<bool> UpdateFish<T>(T fish) where T : class;

        // FishCategory Methods  =====================================================================================
        Task<IEnumerable<FishCategory>> GetAllFishCategories();
        Task<FishCategory?> GetFishCategoryById(int id);
        Task<IEnumerable<T>> GetFishCategory<T>(int fishTypeId) where T : class;

    }
}
