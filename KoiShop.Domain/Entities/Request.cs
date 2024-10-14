using System;
using System.Collections.Generic;

namespace KoiShop.Domain.Entities;

public partial class Request
{
    public int RequestId { get; set; }

    public int? PackageId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? RelationalRequestId { get; set; }

    public DateTime? ConsignmentDate { get; set; }

    public DateTime? EndDate { get; set; }

    public double? AgreementPrice { get; set; }

    public string? TypeRequest { get; set; }

    public string? Status { get; set; }

    public string? UserId { get; set; }
    public virtual User User { get; set; }
    public virtual Package? Package { get; set; }

    public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();
}
