using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Koi, KoiDto>();
            CreateMap<BatchKoi, BatchKoiDto>();
            CreateMap<CartItem, CartDtos>();
            CreateMap<CartDtoV1, CartItem>();
        }
    }
}
