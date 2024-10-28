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
            CreateMap<OrderDetail, OrderDetailDtos>()
                 .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Name : string.Empty))
                 .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Name : string.Empty))
                 .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Image : string.Empty))
                 .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Image : string.Empty));
            CreateMap<OrderDetail, OrderDetailDtoV2>()
                 .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Name : string.Empty))
                 .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Name : string.Empty))
                 .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Image : string.Empty))
                 .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Image : string.Empty))
                 .ForMember(dest => dest.KoiPrice, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Price : 0.0))
                 .ForMember(dest => dest.BatchPrice, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Price : 0.0))
                 .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Certificate : string.Empty));

            CreateMap<Order, OrderDtos>();
            CreateMap<AddKoiDto, Koi>();
            CreateMap<AddBatchKoiDto, BatchKoi>();
            CreateMap<Discount, DiscountDto>();
            CreateMap<Review, ReviewDtos>()
                .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Name : string.Empty))
                 .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Name : string.Empty))
                 .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Image : string.Empty))
                 .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Image : string.Empty)); ;
            CreateMap<Review, ReviewDtoComment>();
            CreateMap<Review, ReviewDtoStar>();
            CreateMap<ReviewDtos, Review>();
            CreateMap<OrderDetailDtoV1, OrderDetail>();
            CreateMap<Request, RequestCareDtos>()
                .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Package.Koi != null ? src.Package.Koi.Name : string.Empty))
                .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.Package.BatchKoi != null ? src.Package.BatchKoi.Name : string.Empty))
                .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Package.Koi != null ? src.Package.Koi.Image : string.Empty))
                .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.Package.BatchKoi != null ? src.Package.BatchKoi.Image : string.Empty));
        }
    }
}
