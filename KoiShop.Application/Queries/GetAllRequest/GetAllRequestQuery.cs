using AutoMapper;
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
    public class GetAllRequestQuery : IRequest<IEnumerable<RequestDtoResponse>>
    {
        
    }
}
