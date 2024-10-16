using AutoMapper;
using KoiShop.Application.Command.UpdatePriceQuotation;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.QuotationDtos
{
    public class QuotationProfile : Profile
    {
        public QuotationProfile() {
            CreateMap<UpdatePriceQuotationCommand, Quotation>();
        }
    }
}
