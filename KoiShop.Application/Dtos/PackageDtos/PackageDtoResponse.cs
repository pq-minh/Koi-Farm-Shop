using KoiShop.Application.Dtos.KoiDtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.PackageDtos
{
    public class PackageDtoResponse
    {

        public int PackageId { get; set; }

        public int? KoiId { get; set; }

        public int? Quantity { get; set; }

        public virtual KoiDtoResponse? Koi { get; set; }

    }
}
