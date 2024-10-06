using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Quotation.Dtos
{
    public class QuotationDto
    {
        public int QuotationId { get; set; }

        public int? RequestId { get; set; }

        public DateOnly? CreateDate { get; set; }

        public double? Price { get; set; }

        public string? Status { get; set; }

        public string? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
