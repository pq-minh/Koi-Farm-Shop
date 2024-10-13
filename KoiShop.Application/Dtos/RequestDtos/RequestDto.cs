using KoiShop.Application.Dtos.QuotationDtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.RequestDtos
{
    public class RequestDto
    {
        public int RequestId { get; set; }

        public int? PackageId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? RelationalRequestId { get; set; }

        public DateTime? ConsignmentDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? AgreementPrice { get; set; }

        public string? TypeRequest { get; set; }

        public string? Status { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<QuotationDto> Quotations { get; set; } = new List<QuotationDto>();
    }
}
