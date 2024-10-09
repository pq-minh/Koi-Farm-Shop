using KoiShop.Domain.Entities ;
using AutoMapper;
namespace KoiShop.Application.AddressDetail.Dtos
{
    public class AddressProfile : Profile
    {
        public AddressProfile() { 
            CreateMap<KoiShop.Domain.Entities.AddressDetail, AddressDetailDtos>();
        }
    }
}
