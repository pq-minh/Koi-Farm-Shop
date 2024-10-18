using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class DiscountDto
    {
        public int DiscountId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? DiscountRate { get; set; }

        public int? TotalQuantity { get; set; }

        public int? Used { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }
    }
    public class DiscountDtoV2
    {
        public string? Name { get; set; }
    }
}
