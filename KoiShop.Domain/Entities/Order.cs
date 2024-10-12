using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public float? TotalAmount { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? OrderStatus { get; set; }

    public int? DiscountId { get; set; }

    public string? ShippingAddress { get; set; }

    public string? PhoneNumber { get; set; }
    public string? UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
