using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.DTOs
{
    public class UpdateKoiDto
    {
        public int? FishTypeId { get; set; }
        public string? KoiName { get; set; }
        public string? Origin { get; set; }
        public string? Description { get; set; }
        public string Gender { get; set; }
        public IFormFile? ImageFile { get; set; } // Chứa tệp hình ảnh
        public int? Age { get; set; }
        public double? Weight { get; set; }
        public double? Size { get; set; }
        public string? Personality { get; set; }
        public string? Status { get; set; }
        public double? Price { get; set; }
        public string? Certificate { get; set; }
    }
}
