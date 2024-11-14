using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double? DiscountRate { get; set; }

    public int? TotalQuantity { get; set; }

    public int? Used { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
