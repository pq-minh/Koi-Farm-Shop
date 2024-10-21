using AutoMapper;
using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Application.Dtos.PackageDtos;
using KoiShop.Application.Dtos.Pagination;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.RequestDtos
{
    public class RequestProfile : Profile
    {
        public RequestProfile() {
            CreateMap<Request, RequestDtoResponse>()
           .ForMember(dest => dest.Package, opt => opt.MapFrom(src => src.Package));

            CreateMap<Package, PackageDtoResponse>()
                .ForMember(dest => dest.Koi, opt => opt.MapFrom(src => src.Koi));

            CreateMap<Koi, KoiDtoResponse>();
            CreateMap<PaginatedResult<Request>, PaginatedResult<RequestDtoResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
