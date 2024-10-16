﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos
{
    public class KoiDto
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

        public string? Status { get; set; }

        public double? Price { get; set; }

        public string? Certificate { get; set; }
        public string? TypeFish { get; set; }
    }
}
