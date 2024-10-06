using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Quotation
{
    public int QuotationId { get; set; }

    public int? RequestId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public double? Price { get; set; }

    public string? Status { get; set; }

    public string? UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual Request? Request { get; set; }
}
