using Datadog.Trace.Ci;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Constant;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using KoiShop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Infrastructure.Respositories
{
    public class UserRepository(KoiShopV1DbContext koiShopV1DbContext, UserManager<User> identityUser) : IUserRepository
    {
            public async Task<PaginatedResult<User>> GetAllUser(int pageNumber, int pageSize)
            {
            var query = from user in koiShopV1DbContext.Users
                        join userRole in koiShopV1DbContext.UserRoles on user.Id equals userRole.UserId
                        join role in koiShopV1DbContext.Roles on userRole.RoleId equals role.Id
                        where role.Name == "Customer"
                        select user;

            var total = await query.CountAsync();


            var items = await query
                     .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();


            return new PaginatedResult<User>(items, total, pageNumber, pageSize);

        }
             public async Task<string> UpdateUser(string userId,string FirstName,string LastName,int point)
             {
                var user = await koiShopV1DbContext.Users.FirstOrDefaultAsync(us => us.Id == userId);
                if (user == null)
                {
                return null;
                }
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Point = point;
                await koiShopV1DbContext.SaveChangesAsync();
                return "Update user succesffully";     
            }
            public async Task<string> DeleteUser(string userID, string reason)
            {
                var user = await koiShopV1DbContext.Users.FirstOrDefaultAsync(us => us.Id == userID);
            if (user == null)
            {
                return null;    
            }
            user.Status = "Banned";
            user.Note = reason;
            await koiShopV1DbContext.SaveChangesAsync();
            return "Delete user succesffully";
            }


        public async Task<string> UnbanUser(string userID)
        {
            var user = await koiShopV1DbContext.Users.FirstOrDefaultAsync(us => us.Id == userID);
            if (user == null)
            {
                return null;
            }
            user.Status = "IsActived";
            user.Note = null;
            await koiShopV1DbContext.SaveChangesAsync();
            return "Unban user succesffully";
        }

        public async Task<PaginatedResult<User>> GetAllUserWithRole(int pageNumber, int pageSize)
        {
            var usersWithRoles = await (from user in koiShopV1DbContext.Users
                                        join userRole in koiShopV1DbContext.UserRoles on user.Id equals userRole.UserId
                                        join role in koiShopV1DbContext.Roles on userRole.RoleId equals role.Id
                                        where role.Name == "Staff" || role.Name == "Customer"
                                        select user)
                                .ToListAsync();
            var totalCount = usersWithRoles.Count;
            var items = usersWithRoles
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
            return new PaginatedResult<User>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<List<string>> GetRoleAsync(string userId)
        {
            var result = await koiShopV1DbContext.Users.FindAsync(userId);
            if ( result == null)
            {
               return new List<string>();
            }
            var roles = await identityUser.GetRolesAsync(result);
            return roles.ToList();
        }
    }
}
