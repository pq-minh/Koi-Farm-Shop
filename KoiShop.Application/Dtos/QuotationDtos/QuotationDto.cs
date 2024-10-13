using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.QuotationDtos
{
    public class QuotationDto
    {
        public int QuotationId { get; set; }

        public int? RequestId { get; set; }
        public double? Price { get; set; }

        public string? Status { get; set; }

        public string? UserId { get; set; }
    }
}
