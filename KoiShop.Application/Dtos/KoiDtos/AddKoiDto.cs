using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.KoiDtos
{
    public class AddKoiDto
    {
        public int FishTypeId { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public IFormFile ImageFile { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Size { get; set; }
        public string Personality { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public IFormFile Certificate { get; set; }
    }
}
