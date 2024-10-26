using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class OrderDetailDtos
    {
        public int? OrderDetailsId { get; set; }

        public int? OrderId { get; set; }

        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? ToTalQuantity { get; set; }

        public string KoiName { get; set; }
        public string BatchKoiName { get; set; }
        public string KoiImage { get; set; }
        public string BatchKoiImage { get; set; }
    }
    public class OrderDetailDtoV1
    {
        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? ToTalQuantity { get; set; }
    }
    public class OrderDetailDtoV2
    {
        public int? OrderDetailsId { get; set; }

        public int? OrderId { get; set; }

        public int? KoiId { get; set; }

        public int? BatchKoiId { get; set; }

        public int? ToTalQuantity { get; set; }

        public string KoiName { get; set; }
        public string BatchKoiName { get; set; }
        public double? KoiPrice { get; set; }
        public double? BatchPrice { get; set; }
        public string KoiImage { get; set; }
        public string BatchKoiImage { get; set; }
    }
}
