using KoiShop.Application.Dtos.PackageDtos;
using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.RequestDtos
{
    public class RequestDtoResponse
    {
        public int RequestId { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? ConsignmentDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public double? AgreementPrice { get; set; }

        public string? TypeRequest { get; set; }
        public virtual PackageDtoResponse? Package { get; set; }
    }
}
