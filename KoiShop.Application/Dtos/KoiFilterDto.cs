using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class KoiFilterDto
    {
        public string? KoiName { get; set; }
        public string? TypeFish { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
        public string? SortBy { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class KoiAndBathKoiId
    {
        public int? KoiId { get; set; }
        public int? BatchKoiId { get; set; }
    }

}
