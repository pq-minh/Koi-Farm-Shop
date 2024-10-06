using KoiShop.Application.Koi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.KoiCategory.Dtos
{
    public class KoiCategoryDto
    {
        public int FishTypeId { get; set; }

        public string? TypeFish { get; set; }

        public virtual ICollection<KoiDto> Kois { get; set; } = new List<KoiDto>();
    }
}
