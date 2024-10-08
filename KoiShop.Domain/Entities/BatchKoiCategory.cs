using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class BatchKoiCategory
{
    public int BatchTypeId { get; set; }

    public string? TypeBatch { get; set; }

    public virtual ICollection<BatchKoi> BatchKois { get; set; } = new List<BatchKoi>();
}
