using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class OrderDetail
{
    public int OrderDetailsId { get; set; }

    public int? OrderId { get; set; }

    public int? KoiId { get; set; }

    public int? BatchKoiId { get; set; }

    public int? ToTalQuantity { get; set; }

    public double? Price { get; set; }
    public string? Status { get; set; }

    public virtual BatchKoi? BatchKoi { get; set; }

    public virtual Koi? Koi { get; set; }

    public virtual Order? Order { get; set; }
}
