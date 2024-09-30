using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Package
{
    public int PackageId { get; set; }

    public int? KoiId { get; set; }

    public int? BatchKoiId { get; set; }

    public int? Quantity { get; set; }

    public virtual BatchKoi? BatchKoi { get; set; }

    public virtual Koi? Koi { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
