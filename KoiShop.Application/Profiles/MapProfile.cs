using AutoMapper;
using KoiShop.Application.Dtos;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Dtos.OrderDtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPayPackage.Enums;

namespace KoiShop.Application.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Koi, KoiDto>()
                .ForMember(dest => dest.TypeFish, opt => opt
                .MapFrom(src => src.FishType != null && src.FishType.TypeFish != null ? src.FishType.TypeFish : string.Empty)).ReverseMap();
            CreateMap<BatchKoi, BatchKoiDto>()
                .ForMember(d => d.TypeFish, o => o
                .MapFrom(src => src.FishType != null && src.FishType.TypeFish != null ? src.FishType.TypeFish : string.Empty));
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
                 .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Certificate : string.Empty));
            CreateMap<OrderDetail, OrderDetailDtoV3>()
                 .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Name : string.Empty))
                 .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Name : string.Empty))
                 .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Image : string.Empty))
                 .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Image : string.Empty))
                 .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.Order != null ? src.Order.CreateDate : (DateTime?)null))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Order != null && src.Order.User != null ? src.Order.User.LastName + " " + src.Order.User.FirstName : string.Empty));
            CreateMap<Order, OrderDtos>()
                  .ForMember(dest => dest.PaymentMethod, 
                   opt => opt.MapFrom(src => src.Payments.FirstOrDefault(p => p.OrderId == src.OrderId) != null
                   ? src.Payments.FirstOrDefault(p => p.OrderId == src.OrderId).PaymenMethod
                   : string.Empty))
                  .ForMember(dest => dest.PaymentStatus,
                     opt => opt.MapFrom(src => src.Payments.FirstOrDefault(p => p.OrderId == src.OrderId) != null
                   ? src.Payments.FirstOrDefault(p => p.OrderId == src.OrderId).Status
                   : string.Empty));
            CreateMap<AddKoiDto, Koi>();
            CreateMap<AddBatchKoiDto, BatchKoi>();
            CreateMap<Discount, DiscountDto>();
            CreateMap<Review, ReviewDtos>()
                .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Name : string.Empty))
                .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Name : string.Empty))
                .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Koi != null ? src.Koi.Image : string.Empty))
                .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.BatchKoi != null ? src.BatchKoi.Image : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.LastName + " " + src.User.FirstName : string.Empty));
            CreateMap<Review, ReviewDtoComment>();
            CreateMap<ReviewDtos, Review>();
            CreateMap<ReviewAllDto, Review>();
            CreateMap<OrderDetailDtoV1, OrderDetail>();
            CreateMap<Request, RequestCareDtos>()
                .ForMember(dest => dest.KoiName, opt => opt.MapFrom(src => src.Package != null && src.Package.Koi != null ? src.Package.Koi.Name : string.Empty))
                .ForMember(dest => dest.BatchKoiName, opt => opt.MapFrom(src => src.Package != null && src.Package.BatchKoi != null ? src.Package.BatchKoi.Name : string.Empty))
                .ForMember(dest => dest.KoiImage, opt => opt.MapFrom(src => src.Package != null && src.Package.Koi != null ? src.Package.Koi.Image : string.Empty))
                .ForMember(dest => dest.BatchKoiImage, opt => opt.MapFrom(src => src.Package != null && src.Package.BatchKoi != null ? src.Package.BatchKoi.Image : string.Empty));
        }
    }
}
