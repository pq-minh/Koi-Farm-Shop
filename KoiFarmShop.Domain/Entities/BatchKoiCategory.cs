using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{
    public partial class BatchKoiCategory
    {
        public int BatchTypeId { get; set; }

        public string? TypeBatch { get; set; }

        public virtual ICollection<BatchKoi> BatchKois { get; set; } = new List<BatchKoi>();
    }
}