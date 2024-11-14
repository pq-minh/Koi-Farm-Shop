using KoiShop.Application.Dtos.Pagination;
using KoiShop.Application.Dtos;
using KoiShop.Application.Queries.GetAllUser;
using KoiShop.Domain.Respositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KoiShop.Application.Queries.GetAllUserWithRole
{
    public class GetAllUserWithRoleQueryHandle(IUserRepository userRepository, UserManager<User> identityUser) : IRequestHandler<GetAllUserWithRoleQuery, PaginatedResult<UserDtoWithRole>>
    {
        public async Task<PaginatedResult<UserDtoWithRole>> Handle(GetAllUserWithRoleQuery request, CancellationToken cancellationToken)
        {
            var userResult = await userRepository.GetAllUserWithRole(request.PageNumber,request.PageSize);
            var userDtoWithRole = new List<UserDtoWithRole>();
            var rolesToCheck = new List<string> { "Customer", "Staff" };

            foreach ( var user in  userResult.Items ) {
                var roles = await userRepository.GetRoleAsync(user.Id);
                if (roles.Intersect(rolesToCheck).Any())
                {
                    userDtoWithRole.Add(new UserDtoWithRole
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = roles.ToList() 
                    });
                }
            }
            return new PaginatedResult<UserDtoWithRole>(
                 userDtoWithRole,
                 userResult.TotalCount,
                 userResult.PageNumber,
                 userResult.PageSize
             );
        }
    }
}
