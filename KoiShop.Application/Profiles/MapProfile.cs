using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
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
            CreateMap<Koi, KoiDto>().ForMember(dest => dest.TypeFish, opt => opt.MapFrom(src => src.FishType.TypeFish));
            CreateMap<BatchKoi, BatchKoiDto>().ForMember(d => d.TypeBatch, o => o.MapFrom(s => s.BatchType.TypeBatch));
            CreateMap<CartItem, CartDtos>();
            CreateMap<CartDtoV1, CartItem>();
            CreateMap<CartDtoV2, CartItem>();
            CreateMap<OrderDetail, OrderDetailDtos>();
            CreateMap<Order, OrderDtos>();
        }
    }
}
