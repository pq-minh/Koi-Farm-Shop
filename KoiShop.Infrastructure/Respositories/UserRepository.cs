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
    public class UserRepository(KoiShopV1DbContext koiShopV1DbContext) : IUserRepository
    {
            public async Task<PaginatedResult<User>> GetAllUser(int pageNumber, int pageSize)
            {
            var userAll = koiShopV1DbContext.Users.AsQueryable(); 


            var items = await userAll
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var total = await userAll.CountAsync();

       
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
            public async Task<string> DeleteUser(string userID)
            {
                var user = await koiShopV1DbContext.Users.FirstOrDefaultAsync(us => us.Id == userID);
            if (user == null)
            {
                return null;    
            }
            user.Status = "Banned";
            await koiShopV1DbContext.SaveChangesAsync();
            return "Delete user succesffully";
            }
    }
}
