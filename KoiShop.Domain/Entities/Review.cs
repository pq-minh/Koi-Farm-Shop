using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public string? Status { get; set; }

    public int? KoiId { get; set; }

    public int? BatchKoiId { get; set; }
    public string? UserId { get; set; }
    public virtual User User { get; set; }

    public virtual BatchKoi? BatchKoi { get; set; }

    public virtual Koi? Koi { get; set; }
}
