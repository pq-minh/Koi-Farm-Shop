using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.AddressDetail.Dtos
{
    public class AddressDetailDtos
    {
        public int AddressId { get; set; }
        public string? City { get; set; }

        public string? Dictrict { get; set; }

        public string? StreetName { get; set; }
        public string? Ward { get; set; }
        public string? Status { get; set;}
    }
}
