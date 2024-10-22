using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KoiShop.Domain.Respositories
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllUser(int pageNumber, int pageSize);
        Task<string> UpdateUser(string userId, string FirstName, string LastName, int point);

        Task<string> DeleteUser(string userID);
    }
}
