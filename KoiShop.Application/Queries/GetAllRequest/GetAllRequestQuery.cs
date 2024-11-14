using AutoMapper;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Application.Dtos.RequestDtos;
using KoiShop.Domain.Entities;
using KoiShop.Domain.Respositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KoiShop.Application.Users.UserContext;

namespace KoiShop.Application.Queries.GetAllRequest
{
    public class GetAllRequestQuery : IRequest<PaginatedResult<RequestDtoResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllRequestQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
