using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class FishCategory
{
    public int FishTypeId { get; set; }

    public string? TypeFish { get; set; }

    public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();
    public virtual ICollection<BatchKoi> BatchKois { get; set; } = new List<BatchKoi>();

}
