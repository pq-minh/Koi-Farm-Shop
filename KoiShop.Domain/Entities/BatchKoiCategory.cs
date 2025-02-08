using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class BatchFishCategory
{
    public int FishTypeId { get; set; }

    public string? TypeBatch { get; set; }

    public virtual ICollection<BatchKoi> BatchKois { get; set; } = new List<BatchKoi>();
}
