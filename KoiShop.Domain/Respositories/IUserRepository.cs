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

        Task<string> DeleteUser(string userID,string reason);

        Task<string> UnbanUser(string userID);
        Task<PaginatedResult<User>> GetAllUserWithRole(int pageNumber, int pageSize);

        Task<List<string>> GetRoleAsync(string userId);
    }
}
