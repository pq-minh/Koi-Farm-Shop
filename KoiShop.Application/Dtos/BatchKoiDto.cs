using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class BatchKoiDto
    {
        public int BatchKoiId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Quantity { get; set; }

        public string? Weight { get; set; }

        public string? Size { get; set; }

        public string? Origin { get; set; }

        public string? Gender { get; set; }

        public string? Age { get; set; }

        public string? Certificate { get; set; }

        public string? Image { get; set; }

        public int? BatchTypeId { get; set; }

        public double? Price { get; set; }

        public string? Status { get; set; }
        public string? TypeBatch { get; set; }
    }
}
