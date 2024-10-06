using KoiShop.Application.Request.Dtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Package.Dtos
{
    public class PackageDto
    {
        public int PackageId { get; set; }

        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? Quantity { get; set; }
    
        public virtual ICollection<RequestDto> Requests { get; set; } = new List<RequestDto>();
    }
}
