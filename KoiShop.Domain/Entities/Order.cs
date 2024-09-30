using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public double? TotalAmount { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? OrderStatus { get; set; }

    public int? DiscountId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public string? UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
