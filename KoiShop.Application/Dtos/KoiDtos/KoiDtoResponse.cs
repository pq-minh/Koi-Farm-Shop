using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.KoiDtos
{
    public class KoiDtoResponse
    {
        public int KoiId { get; set; }

        public int? FishTypeId { get; set; }

        public string? Name { get; set; }

        public string? Origin { get; set; }

        public string? Description { get; set; }

        public string? Gender { get; set; }

        public string? Image { get; set; }

        public int? Age { get; set; }

        public double? Weight { get; set; }

        public double? Size { get; set; }

        public string? Personality { get; set; }

        public double? Price { get; set; }

    }
}
