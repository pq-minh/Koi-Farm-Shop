using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class KoiCategory
    {
        public int FishTypeId { get; set; }

        public string? TypeFish { get; set; }

        public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();
    }
}