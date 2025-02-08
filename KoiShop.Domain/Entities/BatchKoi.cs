using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class BatchKoi
{
    public int BatchKoiId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Quantity { get; set; }

    public string? Weight { get; set; }

    public string? Size { get; set; }

    public string? Origin { get; set; }

    public string? Gender { get; set; }

    public string? Age { get; set; }

    public string? Certificate { get; set; }

    public string? Image { get; set; }

    public int? FishTypeId { get; set; }

    public double? Price { get; set; }

    public string? Status { get; set; }

    public virtual FishCategory? FishType { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public string? UserId { get; set; }
    public virtual User? User { get; set; }
}
